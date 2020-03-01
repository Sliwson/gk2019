#include "camera.h"
#include <glm/gtc/matrix_transform.hpp>

glm::mat4 Camera::GetProjectionMatrix() const
{
	return glm::perspective(glm::radians(45.0f), (float)wndWidth / (float)wndHeight, 0.1f, 100.0f);
}

glm::mat4 Camera::GetViewMatrix() const
{
	return glm::translate(glm::mat4(1.f), -position);
}
