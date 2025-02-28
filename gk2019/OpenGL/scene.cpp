#include "model.h"
#include "bowlingModel.h"
#include "mesh.h"
#include "meshes.h"
#include "vertex.h"
#include "texture.h"
#include "camera.h"
#include "shaders.h"
#include "lights.h"
#include "scene.h"

Scene::Scene(GLFWwindow* window) : window(window) {}

SampleScene::SampleScene(GLFWwindow* window) : Scene(window)
{
	camera1.reset(new MovableCamera({ 0.f, 1.f, 5.f }, 45.f, 0.1f, 100.f, wndWidth, wndHeight));
	camera2.reset(new LookAtCamera({ 0.f, 1.f, 5.f }, 45.f, 0.1f, 100.f, wndWidth, wndHeight));
	camera3.reset(new FollowingCamera({ 2.f, 4.f, 2.f }, 45.f, 0.1f, 100.f, wndWidth, wndHeight));
	currentCamera = camera1;

	gouraudShader.reset(CreateGouraudShader());
	phongShader.reset(CreateNormalShader());
	currentShader = phongShader;
	lightCubeShader.reset(CreateLightCubeShader());
	
	mesh.reset(GetCubeMesh(new Material { 
		32.f, 
		std::unique_ptr<Texture>(new Texture("textures/brick.png", TextureType::Diffuse)),
		std::unique_ptr<Texture>(new Texture("textures/brick.png", TextureType::Specular))
	}));

	pointLight.reset(GetSamplePointLight(mesh.get()));
	spotLight.reset(GetSampleSpotLight());
	dirLight.reset(GetSampleDirectionalLight());

	model.reset(new BowlingModel("models/bowling.obj"));
}

void SampleScene::Update(float dt)
{
	time += dt;
	lastDt = dt;
	ProcessInput();

	const auto background = GetBackgroundColor();
	Clear(background);

	currentShader->SetBool("blinnPhong", useBlinnPhong);
	currentShader->SetBool("useFog", useFog);
	currentShader->SetVector3("backgroundColor", background);

	currentCamera->Update(wndWidth, wndHeight);
	camera2->SetTarget(model->GetSpherePosition());
	camera3->SetTarget(model->GetSpherePosition());

	pointLight->SetColor( { abs(sin(time * 2.0f)), abs(sin(time * 0.7f)), abs(sin(time * 1.3f))});
	pointLight->SetPosition({ sinf(time) * 1.8f, 1.3f, cosf(time) * 1.8f + 2.f });
	pointLight->Use(currentShader.get());
	pointLight->Render(lightCubeShader.get(), currentCamera.get());

	spotLight->SetPosition(currentCamera->GetPosition());
	spotLight->SetDirection(currentCamera->GetFrontVector() + reflectorOffset);
	spotLight->Use(currentShader.get());

	dirLight->Use(currentShader.get());
	dirLight->SetColor(GetDayNightIntensity() * 0.75f);

	//render brick floor
	for (int y = -3; y <= 3; y++)
		for (int x = -3; x <= 3; x++)
			mesh->Draw(currentShader.get(), currentCamera.get(), glm::translate(glm::mat4(1.f), { x, -0.5, y }));

	model->Update(dt);
	model->Render(currentShader.get(), currentCamera.get(), glm::mat4(1.f));
}

void SampleScene::FramebufferSizeCallback(GLFWwindow* window, int width, int height)
{
	glViewport(0, 0, width, height);
	wndWidth = width;
	wndHeight = height;
}

void SampleScene::MouseCallback(GLFWwindow* window, double x, double y)
{
	const auto motionDelta = glm::vec2{ x, y } - previousMousePosition;
	camera1->ProcessMouseEvent(motionDelta);
	previousMousePosition = { x, y };
}

void SampleScene::ScrollCallback(GLFWwindow* window, double x, double y)
{
	camera1->ProcessMouseScroll({ x, y });
}

void SampleScene::KeyCallback(GLFWwindow* window, int key, int scancode, int action, int mods)
{
	const auto isKeyPressed = [&](int keyId) {
		return key == keyId && action == GLFW_PRESS;
	};

	if (isKeyPressed(GLFW_KEY_L))
	{
		dirLight->SetOn(!dirLight->IsOn());
		std::cout << "Directional light state = " << dirLight->IsOn() << std::endl;
	}
	if (isKeyPressed(GLFW_KEY_K))
	{
		pointLight->SetOn(!pointLight->IsOn());
		std::cout << "Point light state = " << pointLight->IsOn() << std::endl;
	}
	if (isKeyPressed(GLFW_KEY_J))
	{
		spotLight->SetOn(!spotLight->IsOn());
		std::cout << "Spot light state = " << spotLight->IsOn() << std::endl;
	}
	if (isKeyPressed(GLFW_KEY_1))
	{
		currentCamera = camera1;
		std::cout << "Camera active = 1" << std::endl;
	}
	if (isKeyPressed(GLFW_KEY_2))
	{
		currentCamera = camera2;
		std::cout << "Camera active = 2" << std::endl;
	}
	if (isKeyPressed(GLFW_KEY_3))
	{
		currentCamera = camera3;
		std::cout << "Camera active = 3" << std::endl;
	}
	if (isKeyPressed(GLFW_KEY_P))
	{
		useBlinnPhong = !useBlinnPhong;
		if (useBlinnPhong)
			std::cout << "Using Blinn model" << std::endl;
		else
			std::cout << "Using Phong model" << std::endl;
	}
	if (isKeyPressed(GLFW_KEY_F))
	{
		useFog = !useFog;
		std::cout << "Use fog = " << useFog << std::endl;
	}
	if (isKeyPressed(GLFW_KEY_G))
	{
		currentShader = gouraudShader;
		std::cout << "Using gouraud shading" << std::endl;
	}
	if (isKeyPressed(GLFW_KEY_N))
	{
		currentShader = phongShader;
		std::cout << "Using phong shading" << std::endl;
	}
}
	
void SampleScene::ProcessInput()
{
	const auto isKeyPressed = [&](int key) {
		return glfwGetKey(window, key) == GLFW_PRESS;
	};

	if (isKeyPressed(GLFW_KEY_ESCAPE))
		glfwSetWindowShouldClose(window, true);
	if (isKeyPressed(GLFW_KEY_W))
		camera1->ProcessKeyboardEvent(CameraDirection::Forward, lastDt);
	if (isKeyPressed(GLFW_KEY_S))
		camera1->ProcessKeyboardEvent(CameraDirection::Backward, lastDt);
	if (isKeyPressed(GLFW_KEY_A))
		camera1->ProcessKeyboardEvent(CameraDirection::Left, lastDt);
	if (isKeyPressed(GLFW_KEY_D))
		camera1->ProcessKeyboardEvent(CameraDirection::Right, lastDt);
	if (isKeyPressed(GLFW_KEY_UP))
		reflectorOffset.y += lastDt;
	if (isKeyPressed(GLFW_KEY_RIGHT))
		reflectorOffset.x += lastDt;
	if (isKeyPressed(GLFW_KEY_DOWN))
		reflectorOffset.y -= lastDt;
	if (isKeyPressed(GLFW_KEY_LEFT))
		reflectorOffset.x -= lastDt;
}

void SampleScene::Clear(glm::vec3 color)
{
	glClearColor(color.x, color.y, color.z, 1.f);
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
}

glm::vec3 SampleScene::GetBackgroundColor() const
{
	if (useFog)
		return { 0.5f, 0.5f, 0.5f };

	const glm::vec3 backgroundColor = { 0.58f, 0.8f, 1.f };
	const auto dayNightColor = GetDayNightIntensity();
	
	return dayNightColor * backgroundColor;
}

glm::vec3 SampleScene::GetDayNightIntensity() const
{
	return glm::vec3(glm::clamp(sin(0.5f * time), -0.5f, 0.5f) + 0.5f);
}
