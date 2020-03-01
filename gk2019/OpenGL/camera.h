#pragma once

#include <glm/glm.hpp>

class Camera
{
public:
	Camera(glm::vec3 position, float fov, float nearPlane, float farPlane, int wndWidth, int wndHeight) :
		position(position), fov(fov), nearPlane(nearPlane), farPlane(farPlane), wndWidth(wndWidth), wndHeight(wndHeight) {}

	glm::mat4 GetProjectionMatrix() const;
	glm::mat4 GetViewMatrix() const;

	void SetPosition(glm::vec3 newPosition) { position = newPosition; }
	glm::vec3 GetPosition() const { return position; }

	void Update(int width, int height) { wndWidth = width; wndHeight = height; }

private:
	glm::vec3 position;
	float fov;
	float nearPlane;
	float farPlane;
	int wndWidth;
	int wndHeight;
};
