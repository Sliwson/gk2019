#include "shader.h"
#include <glm/gtc/type_ptr.hpp>

Shader::Shader(const std::string& vertexShader, const std::string& pixelShader)
{
    id = glCreateProgram();

    const auto vertex = CompileShader(GL_VERTEX_SHADER, vertexShader, id);
    const auto pixel = CompileShader(GL_FRAGMENT_SHADER, pixelShader, id);

    glLinkProgram(id);

    glDetachShader(vertex, id);
    glDetachShader(pixel, id);
    glDeleteShader(vertex);
    glDeleteShader(pixel);
}

Shader::~Shader()
{
    glDeleteProgram(id);
}

void Shader::Use()
{
    glUseProgram(id);
}

void Shader::SetBool(const std::string& name, bool value) const
{
    glUniform1i(glGetUniformLocation(id, name.c_str()), static_cast<GLint>(value));
}

void Shader::SetInt(const std::string& name, int value) const
{
    glUniform1i(glGetUniformLocation(id, name.c_str()), value);
}

void Shader::SetFloat(const std::string& name, float value) const
{
    glUniform1f(glGetUniformLocation(id, name.c_str()), value);
}

void Shader::SetVector3(const std::string& name, glm::vec3 value) const
{
    glUniform3f(glGetUniformLocation(id, name.c_str()), value.r, value.g, value.b);
}

void Shader::SetMatrix3(const std::string& name, glm::mat3 matrix) const
{
    glUniformMatrix3fv(glGetUniformLocation(id, name.c_str()), 1, GL_FALSE, glm::value_ptr(matrix));
}

void Shader::SetMatrix(const std::string& name, glm::mat4 matrix) const
{
    glUniformMatrix4fv(glGetUniformLocation(id, name.c_str()), 1, GL_FALSE, glm::value_ptr(matrix));
}

GLint Shader::CompileShader(GLenum type, const std::string& source, unsigned int shaderProgram)
{
    GLint shader = glCreateShader(type);

    const char* src = source.c_str();
    glShaderSource(shader, 1, &src, nullptr);
    glCompileShader(shader);

    GLint success;
    glGetShaderiv(shader, GL_COMPILE_STATUS, &success);
   
    if (!success)
    {
		char infoLog[512];
        glGetShaderInfoLog(shader, 512, NULL, infoLog);
        std::cout << "Error: " << infoLog << std::endl;
    }

    glAttachShader(shaderProgram, shader);
    return shader;
}
