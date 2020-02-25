#pragma once

#include <glad/glad.h>
#include <iostream>

class Texture
{
public:
	Texture(const std::string& name);
	~Texture();

	void Use();

private:
	GLuint id = 0;
};
