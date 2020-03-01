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

	void FramebufferSizeCallback(GLFWwindow* window, int width, int height)
	{
		glViewport(0, 0, width, height);
		wndWidth = width;
		wndHeight = height;
	}

	void ProcessInput(GLFWwindow* window)
	{
		if (glfwGetKey(window, GLFW_KEY_ESCAPE) == GLFW_PRESS)
			glfwSetWindowShouldClose(window, true);
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

		return window;
	}

	void Clear()
	{
		glClearColor(0.2f, 0.3f, 0.3f, 1.0f);
		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	}

	void MainLoop(GLFWwindow *window)
	{
		std::unique_ptr<Shader> shader(CreateNormalShader());
		std::unique_ptr<Shader> lightCubeShader(CreateLightCubeShader());
		std::unique_ptr<Texture> texture(new Texture("textures/brick.png"));
		std::unique_ptr<Mesh> mesh(GetCubeMesh());
		std::unique_ptr<Light> light(GetSampleLight(mesh.get()));
		std::unique_ptr<Camera> camera(new Camera({ 0.f, 0.f, 5.f }, 45.f, 0.1f, 100.f, wndWidth, wndHeight));

		while (!glfwWindowShouldClose(window))
		{
			auto time = glfwGetTime();
			
			const auto model = glm::rotate(glm::mat4(1.f), (float)time, glm::vec3(.02f, .03f, .0f));

			ProcessInput(window);
			Clear();

			camera->Update(wndWidth, wndHeight);

			light->SetColor( {sin(time * 2.0f), sin(time * 0.7f), sin(time * 1.3f)});
			light->SetPosition({ sinf(time) * 1.8f, 2.f, cosf(time) * 1.8f + 2.f });
			light->Use(shader.get());
			light->Render(lightCubeShader.get(), camera.get());

			texture->Use();
			mesh->Draw(shader.get(), camera.get(), model);

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
