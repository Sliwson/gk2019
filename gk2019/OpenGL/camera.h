#pragma once

#include <glm/glm.hpp>

class Camera
{
public:
	Camera(glm::mat4 position, float fov, float nearPlane, float farPlane) : position(position), fov(fov), nearPlane(nearPlane), farPlane(farPlane) {}

private:
	glm::mat4 position;
	float fov;
	float nearPlane;
	float farPlane;
};
