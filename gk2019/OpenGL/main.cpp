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
	float deltaTime = 0.f;
	glm::vec2 previousMousePosition = { wndWidth / 2, wndHeight / 2 };
	
	std::shared_ptr<Camera> currentCamera;
	std::shared_ptr<DirectionalLight> dirLight;
	std::shared_ptr<PointLight> pointLight;
	std::shared_ptr<SpotLight> spotLight;

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
		const auto isKeyPressed = [&](int key) {
			return glfwGetKey(window, key) == GLFW_PRESS;
		};

		if (isKeyPressed(GLFW_KEY_ESCAPE))
			glfwSetWindowShouldClose(window, true);
		if (isKeyPressed(GLFW_KEY_W))
			currentCamera->ProcessKeyboardEvent(CameraDirection::Forward, deltaTime);
		if (isKeyPressed(GLFW_KEY_S))
			currentCamera->ProcessKeyboardEvent(CameraDirection::Backward, deltaTime);
		if (isKeyPressed(GLFW_KEY_A))
			currentCamera->ProcessKeyboardEvent(CameraDirection::Left, deltaTime);
		if (isKeyPressed(GLFW_KEY_D))
			currentCamera->ProcessKeyboardEvent(CameraDirection::Right, deltaTime);
		
	}

	void KeyCallback(GLFWwindow* window, int key, int scancode, int action, int mods)
	{
		if (key == GLFW_KEY_L && action == GLFW_PRESS)
		{
			dirLight->SetOn(!dirLight->IsOn());
			std::cout << "Directional light state = " << dirLight->IsOn() << std::endl;
		}
		if (key == GLFW_KEY_K && action == GLFW_PRESS)
		{
			pointLight->SetOn(!pointLight->IsOn());
			std::cout << "Point light state = " << pointLight->IsOn() << std::endl;
		}
		if (key == GLFW_KEY_J && action == GLFW_PRESS)
		{
			spotLight->SetOn(!spotLight->IsOn());
			std::cout << "Spot light state = " << spotLight->IsOn() << std::endl;
		}
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
		glfwSetKeyCallback(window, KeyCallback);
		glfwSetCursorPosCallback(window, MouseCallback);

		glfwSetInputMode(window, GLFW_CURSOR, GLFW_CURSOR_DISABLED);

		return window;
	}

	void Clear(glm::vec3 fadeColor)
	{
		const glm::vec3 backgroundColor = { 0.58f, 0.8f, 1.f };
		const auto color = backgroundColor * fadeColor;
		glClearColor(color.x, color.y, color.z, 1.f);
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
		pointLight.reset(GetSamplePointLight(mesh.get()));
		spotLight.reset(GetSampleSpotLight());
		dirLight.reset(GetSampleDirectionalLight());
		
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
			
			glm::vec3 dayNightColor = glm::vec3(glm::clamp(sin(0.5f * time), -0.5f, 0.5f) + 0.5f);
			ProcessInput(window);
			Clear(dayNightColor);

			currentCamera->Update(wndWidth, wndHeight);

			pointLight->SetColor( { abs(sin(time * 2.0f)), abs(sin(time * 0.7f)), abs(sin(time * 1.3f))});
			pointLight->SetPosition({ sinf(time) * 1.8f, 1.f, cosf(time) * 1.8f + 2.f });
			pointLight->Use(shader.get());
			pointLight->Render(lightCubeShader.get(), currentCamera.get());

			spotLight->SetPosition(currentCamera->GetPosition());
			spotLight->SetDirection(currentCamera->GetFrontVector());
			spotLight->Use(shader.get());

			dirLight->Use(shader.get());
			dirLight->SetColor(dayNightColor);

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
