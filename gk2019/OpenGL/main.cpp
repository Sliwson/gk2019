#include "scene.h"

namespace {
	constexpr int wndWidth = 800;
	constexpr int wndHeight = 600;
	std::unique_ptr<Scene> scene;

	void FramebufferSizeCallback(GLFWwindow* window, int width, int height)
	{
		scene->FramebufferSizeCallback(window, width, height);
	}

	void MouseCallback(GLFWwindow* window, double x, double y)
	{
		scene->MouseCallback(window, x, y);
	}

	void ScrollCallback(GLFWwindow* window, double x, double y)
	{
		scene->ScrollCallback(window, x, y);
	}

	void KeyCallback(GLFWwindow* window, int key, int scancode, int action, int mods)
	{
		scene->KeyCallback(window, key, scancode, action, mods);
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
		glfwSetInputMode(window, GLFW_CURSOR, GLFW_CURSOR_DISABLED);

		return window;
	}
	
	void InitCallbacks(GLFWwindow* window)
	{
		glfwSetFramebufferSizeCallback(window, FramebufferSizeCallback);
		glfwSetScrollCallback(window, ScrollCallback);
		glfwSetKeyCallback(window, KeyCallback);
		glfwSetCursorPosCallback(window, MouseCallback);
	}

	void MainLoop(GLFWwindow *window)
	{

		float prevTime = static_cast<float>(glfwGetTime());
		while (!glfwWindowShouldClose(window))
		{
			float time = static_cast<float>(glfwGetTime());
			float dt = time - prevTime;
			prevTime = time;

			scene->Update(dt);
			
			glfwSwapBuffers(window);
			glfwPollEvents();
		}
	}
}

int main(int argc, char** argv)
{
    auto* window = InitWindowSystem();
	scene.reset(new SampleScene(window));

    if (window == nullptr)
        return -1;

	InitCallbacks(window);
    MainLoop(window);

    glfwTerminate();
	return 0;
}
