#pragma once

#include <glad/glad.h>
#include <glm/glm.hpp>
#include <vector>

#include "material.h"

class Shader;
class Camera;
struct Vertex;

class Mesh
{
public:
	Mesh(std::vector<Vertex> vertices, std::vector<GLuint> indices, Material* material);
	virtual void Draw(Shader* shader, Camera* camera, const glm::mat4& model);
	virtual ~Mesh();

protected:
	virtual void Init();

	std::vector<Vertex> vertices;
	std::vector<GLuint> indices;
	std::unique_ptr<Material> material;

	GLuint VAO, VBO, EBO;
};
