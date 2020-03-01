#pragma once

#include "shader.h"

Shader* CreateNormalShader()
{
	const std::string vertexShaderSource =
#include "vertex.vs"
		;
	const std::string pixelShaderSource =
#include "pixel.ps"
		;

	return new Shader(vertexShaderSource, pixelShaderSource);
}

Shader* CreateLightCubeShader()
{
	const std::string vertexShaderSource =
#include "light.vs"
		;
	const std::string pixelShaderSource =
#include "light.ps"
		;

	return new Shader(vertexShaderSource, pixelShaderSource);
}
