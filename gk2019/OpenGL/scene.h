#pragma once

#include <glad/glad.h>
#include <GLFW/glfw3.h>
#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <iostream>
#include <algorithm>
#include <numeric>

class Shader;
class Texture;
class Mesh;
class BowlingModel;
class Camera;
class MovableCamera;
class LookAtCamera;
class FollowingCamera;
class DirectionalLight;
class PointLight;
class SpotLight;

class Scene
{
public:
	Scene(GLFWwindow* window);
	virtual void Update(float dt) = 0;
	virtual void FramebufferSizeCallback(GLFWwindow* window, int width, int height) = 0;
	virtual void MouseCallback(GLFWwindow* window, double x, double y) = 0;
	virtual void ScrollCallback(GLFWwindow* window, double x, double y) = 0;
	virtual void KeyCallback(GLFWwindow* window, int key, int scancode, int action, int mods) = 0;

protected:
	GLFWwindow* window = nullptr;
	int wndWidth = 800;
	int wndHeight = 600;
	float lastDt = 0.f;
	
};

class SampleScene : public Scene
{
public:
	SampleScene(GLFWwindow* window);
	virtual void FramebufferSizeCallback(GLFWwindow* window, int width, int height);
	virtual void MouseCallback(GLFWwindow* window, double x, double y);
	virtual void ScrollCallback(GLFWwindow* window, double x, double y);
	virtual void KeyCallback(GLFWwindow* window, int key, int scancode, int action, int mods);
	virtual void Update(float dt);

protected:
	virtual void ProcessInput();
	virtual void Clear(glm::vec3 fadeColor);
	
	std::unique_ptr<Shader> shader;
	std::unique_ptr<Shader> lightCubeShader;
	std::unique_ptr<Texture> whiteSpec;
	std::unique_ptr<Texture> whiteDiff;
	std::unique_ptr<Mesh> mesh;
	std::unique_ptr<BowlingModel> model;

	std::shared_ptr<Camera> currentCamera;
	std::shared_ptr<MovableCamera> camera1;
	std::shared_ptr<LookAtCamera> camera2;
	std::shared_ptr<FollowingCamera> camera3;
	std::shared_ptr<DirectionalLight> dirLight;
	std::shared_ptr<PointLight> pointLight;
	std::shared_ptr<SpotLight> spotLight;

	glm::vec2 previousMousePosition = { wndWidth / 2, wndHeight / 2 };
	float time = 0;
};
