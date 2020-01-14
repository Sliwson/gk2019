#include <glad/glad.h>
#include <GLFW/glfw3.h>
#include <iostream>

#include "tutorial.h"

void framebuffer_size_callback(GLFWwindow* window, int width, int height)
{
    glViewport(0, 0, width, height);
}

void processInput(GLFWwindow* window)
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

    GLFWwindow* window = glfwCreateWindow(800, 600, "Bowling", NULL, NULL);
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
   
    glViewport(0, 0, 800, 600);
    glfwSetFramebufferSizeCallback(window, framebuffer_size_callback);

    return window;
}

void MainLoop(GLFWwindow *window)
{
	auto program = CompileShaders();
	auto triangle = GetTriangleVao();
    auto colorUniform = glGetUniformLocation(program, "objectColor");
    
    while (!glfwWindowShouldClose(window))
    {
        auto time = glfwGetTime();

        processInput(window);

        Clear();

        glUniform4f(colorUniform, 0.f, .5f * sin(time) + 0.5f, 0.f, 1.f);
        //glPolygonMode(GL_FRONT_AND_BACK, GL_LINE);
        glBindVertexArray(triangle);
        glDrawElements(GL_TRIANGLES, 6, GL_UNSIGNED_INT, 0);

        glfwSwapBuffers(window);
        glfwPollEvents();
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