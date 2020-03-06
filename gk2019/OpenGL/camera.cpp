#include "camera.h"
#include <glm/gtc/matrix_transform.hpp>

glm::mat4 Camera::GetProjectionMatrix() const
{
	return glm::perspective(glm::radians(zoom), (float)wndWidth / (float)wndHeight, 0.1f, 100.0f);
}

glm::mat4 Camera::GetViewMatrix() const
{
	return glm::lookAt(position, position + front, up);
}

void Camera::Update(int width, int height)
{
	wndWidth = width;
	wndHeight = height;

	front = glm::normalize(glm::vec3{ cos(glm::radians(yaw)) * cos(glm::radians(pitch)), sin(glm::radians(pitch)), sin(glm::radians(yaw)) * cos(glm::radians(pitch)) });
	right = glm::normalize(glm::cross(front, worldUp));
	up = glm::normalize(glm::cross(right, front));
}

void MovableCamera::ProcessKeyboardEvent(CameraDirection direction, float dt)
{
	float velocity = movementSpeed * dt;
	switch (direction)
	{
	case CameraDirection::Forward:
		position += front * velocity;
		break;
	case CameraDirection::Backward:
		position -= front * velocity;
		break;
	case CameraDirection::Left:
		position -= right * velocity;
		break;
	case CameraDirection::Right:
		position += right * velocity;
	}
}

void MovableCamera::ProcessMouseEvent(glm::vec2 offset)
{
	offset *= mouseSensitivity;

	yaw += offset.x;
	pitch -= offset.y;
	pitch = glm::clamp(pitch, -89.f, 89.f);
}

void MovableCamera::ProcessMouseScroll(glm::vec2 offset)
{
	zoom -= offset.y;
	zoom = glm::clamp(zoom, 1.f, 45.f);
}

void LookAtCamera::SetTarget(glm::vec3 target)
{
	front = target - position;
}

void FollowingCamera::SetTarget(glm::vec3 target)
{
	position = target + offset;
	front = target - position;
}
