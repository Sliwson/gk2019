#include <glad/glad.h>
#include <GLFW/glfw3.h>
#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <iostream>

#include "tutorial.h"

namespace {
    int wndWidth = 800;
    int wndHeight = 600;
}

void framebuffer_size_callback(GLFWwindow* window, int width, int height)
{
    glViewport(0, 0, width, height);
    wndWidth = width;
    wndHeight = height;
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
   
    glViewport(0, 0, 800, 600);
    glfwSetFramebufferSizeCallback(window, framebuffer_size_callback);

    return window;
}

void MainLoop(GLFWwindow *window)
{
    glEnable(GL_DEPTH_TEST);

    std::unique_ptr<Shader> shader(CreateShader());
    std::unique_ptr<Texture> texture(new Texture("textures/brick.png"));

	auto cube = GetCube();
    
    while (!glfwWindowShouldClose(window))
    {
        shader->Use();
        texture->Use();

        auto time = glfwGetTime();
        auto model = glm::rotate(glm::mat4(1.f), (float)time * glm::pi<float>(), glm::vec3(.1f, .1f, .0f));
        const auto view = glm::translate(glm::mat4(1.f), glm::vec3(0.0f, 0.0f, -3.0f));
        const auto projection = glm::perspective(glm::radians(45.0f), (float)wndWidth / (float)wndHeight, 0.1f, 100.0f);
        
        shader->SetMatrix("model", model);
        shader->SetMatrix("projection", projection);
        shader->SetMatrix("view", view);

        processInput(window);

        Clear();

        glBindVertexArray(cube);
        glDrawArrays(GL_TRIANGLES, 0, 36);

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
