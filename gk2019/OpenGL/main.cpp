#include <glad/glad.h>
#include <GLFW/glfw3.h>
#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <iostream>
#include <algorithm>
#include <numeric>

#include "mesh.h"
#include "meshes.h"
#include "shader.h"
#include "vertex.h"
#include "texture.h"
#include "light.h"
#include "camera.h"
#include "shaders.h"
#include "lights.h"

namespace {
	int wndWidth = 800;
	int wndHeight = 600;
	std::shared_ptr<Camera> currentCamera;
	float deltaTime = 0.f;
	glm::vec2 previousMousePosition = { wndWidth / 2, wndHeight / 2 };

	void FramebufferSizeCallback(GLFWwindow* window, int width, int height)
	{
		glViewport(0, 0, width, height);
		wndWidth = width;
		wndHeight = height;
	}

	void MouseCallback(GLFWwindow* window, double x, double y)
	{
		const auto motionDelta = glm::vec2{ x, y } - previousMousePosition;
		currentCamera->ProcessMouseEvent(motionDelta);
		previousMousePosition = { x, y };
	}

	void ScrollCallback(GLFWwindow* window, double x, double y)
	{
		currentCamera->ProcessMouseScroll({ x, y });
	}

	void ProcessInput(GLFWwindow* window)
	{
		if (glfwGetKey(window, GLFW_KEY_ESCAPE) == GLFW_PRESS)
			glfwSetWindowShouldClose(window, true);
		if (glfwGetKey(window, GLFW_KEY_W) == GLFW_PRESS)
			currentCamera->ProcessKeyboardEvent(CameraDirection::Forward, deltaTime);
		if (glfwGetKey(window, GLFW_KEY_S) == GLFW_PRESS)
			currentCamera->ProcessKeyboardEvent(CameraDirection::Backward, deltaTime);
		if (glfwGetKey(window, GLFW_KEY_A) == GLFW_PRESS)
			currentCamera->ProcessKeyboardEvent(CameraDirection::Left, deltaTime);
		if (glfwGetKey(window, GLFW_KEY_D) == GLFW_PRESS)
			currentCamera->ProcessKeyboardEvent(CameraDirection::Right, deltaTime);
	}

	GLFWwindow* InitWindowSystem()
	{
		glfwInit();
		glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 4);
		glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 6);
		glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);

		GLFWwindow* window = glfwCreateWindow(wndWidth, wndHeight, "Bowling", NULL, NULL);
		if (window == nullptr)
		{
			std::cout << "Failed to create GLFW window" << std::endl;
			glfwTerminate();
			return nullptr;
		}

		glfwMakeContextCurrent(window);

		if (!gladLoadGLLoader((GLADloadproc)glfwGetProcAddress))
		{
			std::cout << "Failed to initialize GLAD" << std::endl;
			glfwTerminate();
			return nullptr;
		}

		glViewport(0, 0, wndWidth, wndHeight);
		glEnable(GL_DEPTH_TEST);

		glfwSetFramebufferSizeCallback(window, FramebufferSizeCallback);
		glfwSetScrollCallback(window, ScrollCallback);
		glfwSetCursorPosCallback(window, MouseCallback);

		glfwSetInputMode(window, GLFW_CURSOR, GLFW_CURSOR_DISABLED);

		return window;
	}

	void Clear()
	{
		glClearColor(0.2f, 0.3f, 0.3f, 1.0f);
		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	}

	void MainLoop(GLFWwindow *window)
	{
		currentCamera.reset(new Camera({ 0.f, 0.f, 5.f }, 45.f, 0.1f, 100.f, wndWidth, wndHeight));
		std::unique_ptr<Shader> shader(CreateNormalShader());
		std::unique_ptr<Shader> lightCubeShader(CreateLightCubeShader());
		std::unique_ptr<Texture> diffuseTex(new Texture("textures/brick.png", TextureType::Diffuse));
		std::unique_ptr<Texture> specularTex(new Texture("textures/brick.png", TextureType::Specular));
		std::unique_ptr<Mesh> mesh(GetCubeMesh());
		std::unique_ptr<Light> light(GetSampleLight(mesh.get()));
		
		glm::vec3 cubePositions[] = {
			glm::vec3(0.0f,  0.0f,  0.0f),
			glm::vec3(2.0f,  5.0f, -15.0f),
			glm::vec3(-1.5f, -2.2f, -2.5f),
			glm::vec3(-3.8f, -2.0f, -12.3f),
			glm::vec3(2.4f, -0.4f, -3.5f),
			glm::vec3(-1.7f,  3.0f, -7.5f),
			glm::vec3(1.3f, -2.0f, -2.5f),
			glm::vec3(1.5f,  2.0f, -2.5f),
			glm::vec3(1.5f,  0.2f, -1.5f),
			glm::vec3(-1.3f,  1.0f, -1.5f)
		};

		float prevTime = static_cast<float>(glfwGetTime());
		while (!glfwWindowShouldClose(window))
		{
			float time = static_cast<float>(glfwGetTime());
			deltaTime = time - prevTime;
			prevTime = time;
			
			ProcessInput(window);
			Clear();

			currentCamera->Update(wndWidth, wndHeight);

			light->SetColor( { abs(sin(time * 2.0f)), abs(sin(time * 0.7f)), abs(sin(time * 1.3f))});
			light->SetPosition({ sinf(time) * 1.8f, 1.f, cosf(time) * 1.8f + 2.f });
			light->Use(shader.get());
			light->Render(lightCubeShader.get(), currentCamera.get());

			diffuseTex->Use(shader.get());
			specularTex->Use(shader.get());

			for (int i = 0; i < 10; i++)
			{
				const auto model = glm::translate(glm::mat4(1.f), cubePositions[i]) * glm::rotate(glm::mat4(1.f), (float)(time + 1.f * i), glm::vec3(.02f, .03f, .0f)); 
				mesh->Draw(shader.get(), currentCamera.get(), model);
			}

			glfwSwapBuffers(window);
			glfwPollEvents();
		}
	}
}


int main(int argc, char** argv)
{
    auto* window = InitWindowSystem();
    if (window == nullptr)
        return -1;

    MainLoop(window);

    glfwTerminate();
	return 0;
}
