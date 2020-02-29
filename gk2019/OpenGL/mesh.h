#pragma once

#include <glad/glad.h>
#include <glm/glm.hpp>
#include <vector>

class Shader;
struct Vertex;

class Mesh
{
public:
	Mesh(std::vector<Vertex> vertices, std::vector<GLuint> indices);
	virtual void Draw(Shader* shader, const glm::mat4& model, const glm::mat4& view, const glm::mat4& projection);
	virtual ~Mesh();

protected:
	virtual void Init();

	std::vector<Vertex> vertices;
	std::vector<GLuint> indices;
	GLuint VAO, VBO, EBO;
};
