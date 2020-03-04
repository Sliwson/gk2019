#pragma once

#include "light.h"
#include "mesh.h"

PointLight* GetSamplePointLight(Mesh* mesh)
{
	return new PointLight(mesh, { -0.6f, .7f, 1.8f }, { 1.f, 1.f, 0.5f }, glm::vec3(0.05f), glm::vec3(0.5f), glm::vec3(1.f));
}

SpotLight* GetSampleSpotLight()
{
	return new SpotLight(nullptr, { 0.f, 0.f, 0.f}, { 1.f, 1.f, 0.5f }, glm::vec3(0.05f), glm::vec3(0.5f), glm::vec3(1.f), { 0, 1.f, 1.f }, glm::radians(12.5f));
}

DirectionalLight* GetSampleDirectionalLight()
{
	return new DirectionalLight(nullptr, { 0.3f, 1.f, -0.3f }, { 0.3f, 0.3f, 0.3f }, glm::vec3(0.1f), glm::vec3(1.f), glm::vec3(0.1f));
}
