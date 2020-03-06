#pragma once

#include <glm/glm.hpp>

enum class CameraDirection
{
	Forward,
	Backward,
	Left,
	Right
};

class Camera
{
public:
	Camera(glm::vec3 position, float fov, float nearPlane, float farPlane, int wndWidth, int wndHeight) :
		position(position), zoom(fov), nearPlane(nearPlane), farPlane(farPlane), wndWidth(wndWidth), wndHeight(wndHeight) {}

	virtual glm::mat4 GetProjectionMatrix() const;
	virtual glm::mat4 GetViewMatrix() const;

	void SetPosition(glm::vec3 newPosition) { position = newPosition; }
	glm::vec3 GetPosition() const { return position; }
	glm::vec3 GetFrontVector() const { return front; }

	virtual void Update(int width, int height);

protected:
	glm::vec3 position = { 0.f, 0.f, 0.f };
	glm::vec3 front = { 0.f, 0.f, -1.f };
	glm::vec3 up = { 0.f, 1.f, 0.f };
	glm::vec3 right = { 1.f, 0.f, 0.f };
	glm::vec3 worldUp = { 0.f, 1.f, 0.f };

	float nearPlane = 0.1f;
	float farPlane = 100.f;

	int wndWidth = 800;
	int wndHeight = 600;

	float yaw = -90.f;
	float pitch = 0.f;
	float movementSpeed = 1.5f;
	float mouseSensitivity = 0.02f;
	float zoom = 45.f;
};

class MovableCamera : public Camera
{
public:
	MovableCamera(glm::vec3 position, float fov, float nearPlane, float farPlane, int wndWidth, int wndHeight) : Camera(position, fov, nearPlane, farPlane, wndWidth, wndHeight) {};

	void ProcessKeyboardEvent(CameraDirection direction, float dt);
	void ProcessMouseEvent(glm::vec2 offset);
	void ProcessMouseScroll(glm::vec2 offset);
};

class LookAtCamera : public Camera
{
public:
	LookAtCamera(glm::vec3 position, float fov, float nearPlane, float farPlane, int wndWidth, int wndHeight) : Camera(position, fov, nearPlane, farPlane, wndWidth, wndHeight) {};
	void SetTarget(glm::vec3 position);
};

class FollowingCamera : public Camera
{
public:
	FollowingCamera(glm::vec3 offset, float fov, float nearPlane, float farPlane, int wndWidth, int wndHeight) : Camera(glm::vec3(0.f), fov, nearPlane, farPlane, wndWidth, wndHeight), offset(offset) {};

	void SetTarget(glm::vec3 position);
private:
	glm::vec3 offset;
};

