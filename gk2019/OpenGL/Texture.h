#pragma once

#include <glad/glad.h>
#include <iostream>

class Shader;

enum class TextureType
{
	Diffuse,
	Specular
};

class Texture
{
public:
	Texture(const std::string& name, TextureType type);
	~Texture();

	void Use(Shader* shader);

private:
	GLuint id = 0;
	TextureType type;
};
