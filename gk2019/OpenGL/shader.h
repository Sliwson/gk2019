#pragma once

#include <glad/glad.h>
#include <iostream>
#include <string>

class Shader
{
public:
	Shader(const std::string& vertexShader, const std::string& pixelShader);
	~Shader();

	void Use();
	void SetBool(const std::string& name, bool value) const;
	void SetInt(const std::string& name, int value) const;
	void SetFloat(const std::string& name, float value) const;

private:
	GLint CompileShader(GLenum type, const std::string& source, unsigned int shaderProgram);

	GLint id = 0;
};
