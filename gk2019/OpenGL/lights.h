#pragma once

#include "light.h"
#include "mesh.h"

Light* GetSampleLight(Mesh* mesh)
{
	return new Light(mesh, { -0.6f, .7f, 1.8f }, { 1.f, 1.f, 0.5f }, glm::vec3(0.2f), glm::vec3(0.5f), glm::vec3(1.f));
}
