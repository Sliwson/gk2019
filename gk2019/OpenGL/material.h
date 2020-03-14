#pragma once

#include <glm/glm.hpp>
#include "texture.h"

struct Material
{
	float shininess;
	std::unique_ptr<Texture> diffuse;
	std::unique_ptr<Texture> specular;
};
