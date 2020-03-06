#include <glad/glad.h>
#include <GLFW/glfw3.h>
#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <iostream>
#include <algorithm>
#include <numeric>

#include "model.h"
#include "bowlingModel.h"
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
	std::shared_ptr<MovableCamera> camera1;
	std::shared_ptr<LookAtCamera> camera2;
	std::shared_ptr<FollowingCamera> camera3;
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
		camera1->ProcessMouseEvent(motionDelta);
		previousMousePosition = { x, y };
	}

	void ScrollCallback(GLFWwindow* window, double x, double y)
	{
		camera1->ProcessMouseScroll({ x, y });
	}

	void ProcessInput(GLFWwindow* window)
	{
		const auto isKeyPressed = [&](int key) {
			return glfwGetKey(window, key) == GLFW_PRESS;
		};

		if (isKeyPressed(GLFW_KEY_ESCAPE))
			glfwSetWindowShouldClose(window, true);

		if (isKeyPressed(GLFW_KEY_W))
			camera1->ProcessKeyboardEvent(CameraDirection::Forward, deltaTime);
		if (isKeyPressed(GLFW_KEY_S))
			camera1->ProcessKeyboardEvent(CameraDirection::Backward, deltaTime);
		if (isKeyPressed(GLFW_KEY_A))
			camera1->ProcessKeyboardEvent(CameraDirection::Left, deltaTime);
		if (isKeyPressed(GLFW_KEY_D))
			camera1->ProcessKeyboardEvent(CameraDirection::Right, deltaTime);
		
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
		if (key == GLFW_KEY_1 && action == GLFW_PRESS)
		{
			currentCamera = camera1;
			std::cout << "Camera active = 1" << std::endl;
		}
		if (key == GLFW_KEY_2 && action == GLFW_PRESS)
		{
			currentCamera = camera2;
			std::cout << "Camera active = 2" << std::endl;
		}
		if (key == GLFW_KEY_3 && action == GLFW_PRESS)
		{
			currentCamera = camera3;
			std::cout << "Camera active = 3" << std::endl;
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
		camera1.reset(new MovableCamera({ 0.f, 1.f, 5.f }, 45.f, 0.1f, 100.f, wndWidth, wndHeight));
		camera2.reset(new LookAtCamera({ 0.f, 1.f, 5.f }, 45.f, 0.1f, 100.f, wndWidth, wndHeight));
		camera3.reset(new FollowingCamera({ 2.f, 4.f, 2.f }, 45.f, 0.1f, 100.f, wndWidth, wndHeight));
		currentCamera = camera1;

		std::unique_ptr<Shader> shader(CreateNormalShader());
		std::unique_ptr<Shader> lightCubeShader(CreateLightCubeShader());
		
		std::unique_ptr<Texture> whiteSpec(new Texture("textures/white.png", TextureType::Specular));
		std::unique_ptr<Texture> whiteDiff(new Texture("textures/white.png", TextureType::Diffuse));

		std::unique_ptr<Mesh> mesh(GetCubeMesh(new Material { 
			32.f, 
			std::unique_ptr<Texture>(new Texture("textures/brick.png", TextureType::Diffuse)),
			std::unique_ptr<Texture>(new Texture("textures/brick.png", TextureType::Specular))
		}));

		pointLight.reset(GetSamplePointLight(mesh.get()));
		spotLight.reset(GetSampleSpotLight());
		dirLight.reset(GetSampleDirectionalLight());

		std::unique_ptr<BowlingModel> model(new BowlingModel("models/bowling.obj"));

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
			camera2->SetTarget(model->GetSpherePosition());
			camera3->SetTarget(model->GetSpherePosition());

			pointLight->SetColor( { abs(sin(time * 2.0f)), abs(sin(time * 0.7f)), abs(sin(time * 1.3f))});
			pointLight->SetPosition({ sinf(time) * 1.8f, 1.3f, cosf(time) * 1.8f + 2.f });
			pointLight->Use(shader.get());
			pointLight->Render(lightCubeShader.get(), currentCamera.get());

			spotLight->SetPosition(currentCamera->GetPosition());
			spotLight->SetDirection(currentCamera->GetFrontVector());
			spotLight->Use(shader.get());

			dirLight->Use(shader.get());
			dirLight->SetColor(dayNightColor);

			//render brick floor
			for (int y = -3; y <= 3; y++)
				for (int x = -3; x <= 3; x++)
					mesh->Draw(shader.get(), currentCamera.get(), glm::translate(glm::mat4(1.f), { x, -0.5, y }));

			//my model has empty textures, so override
			whiteDiff->Use(shader.get());
			whiteSpec->Use(shader.get());
			model->Update(deltaTime);
			model->Render(shader.get(), currentCamera.get(), glm::mat4(1.f));

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
