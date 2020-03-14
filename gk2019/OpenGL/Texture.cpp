#include "texture.h"
#include "shader.h"

#define STB_IMAGE_IMPLEMENTATION
#include "stb_image.h"
 
Texture::Texture(const std::string& name, TextureType type)
{
	this->type = type;

	int width, height, nrChannels;
	unsigned char* data = stbi_load(name.c_str(), &width, &height, &nrChannels, 0);
	
	if (data == nullptr)
	{
		std::cout << "Failed to load " << name << std::endl;
		return;
	}

	glGenTextures(1, &id);
	glBindTexture(GL_TEXTURE_2D, id);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, width, height, 0, GL_RGB, GL_UNSIGNED_BYTE, data);
	glBindTexture(GL_TEXTURE_2D, 0);

	stbi_image_free(data);
}

Texture::~Texture()
{
	glDeleteTextures(1, &id);
}

void Texture::Use(Shader* shader)
{
	switch (type)
	{
	case TextureType::Diffuse:
		shader->SetInt("material.diffuse", 0);
		glActiveTexture(GL_TEXTURE0);
		break;
	case TextureType::Specular:
		shader->SetInt("material.specular", 1);
		glActiveTexture(GL_TEXTURE1);
		break;
	}

	glBindTexture(GL_TEXTURE_2D, id);
}
