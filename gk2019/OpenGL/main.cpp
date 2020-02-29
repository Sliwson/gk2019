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

	Shader* CreateNormalShader()
	{
		const std::string vertexShaderSource =
#include "vertex.vs"
			;
		const std::string pixelShaderSource =
#include "pixel.ps"
			;

		return new Shader(vertexShaderSource, pixelShaderSource);
	}

	Shader* CreateLightCubeShader()
	{
		const std::string vertexShaderSource =
#include "light.vs"
			;
		const std::string pixelShaderSource =
#include "light.ps"
			;

		return new Shader(vertexShaderSource, pixelShaderSource);
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
		std::unique_ptr<Light> light(new Light(mesh.get(), { -0.6f, .7f, 2.8f }, { 1.f, 1.f, 0.5f }));

		while (!glfwWindowShouldClose(window))
		{
			auto time = glfwGetTime();
			
			const auto model = glm::rotate(glm::mat4(1.f), (float)time, glm::vec3(.02f, .03f, .0f));
			const auto view = glm::translate(glm::mat4(1.f), glm::vec3(0.0f, 0.0f, -3.0f));
			const auto projection = glm::perspective(glm::radians(45.0f), (float)wndWidth / (float)wndHeight, 0.1f, 100.0f);

			ProcessInput(window);
			Clear();

			texture->Use();
			light->Use(shader.get());
			mesh->Draw(shader.get(), model, view, projection);

			light->Render(lightCubeShader.get(), view, projection);

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
