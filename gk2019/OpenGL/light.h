#pragma once

#include <memory>
#include <glm/glm.hpp>
#include <glm/geometric.hpp>
#include <glm/gtc/matrix_transform.hpp>

class Mesh;
class Shader;
class Camera;

class Light
{
public:
	Light(Mesh* mesh, glm::vec3 position, glm::vec3 color) : mesh(mesh), position(position), color(color) { }

	void Render(Shader* shader, Camera* camera);
	void Use(Shader* shader);

	void SetPosition(glm::vec3 newPosition) { position = newPosition; }

private:
	glm::vec3 position;
	glm::vec3 color;
	Mesh* mesh;

	const float scale = 0.1f;
};
