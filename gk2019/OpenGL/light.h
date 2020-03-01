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
	Light(Mesh* mesh, glm::vec3 position, glm::vec3 color, glm::vec3 ambient, glm::vec3 diffuse, glm::vec3 specular) : 
		mesh(mesh), position(position), color(color), ambient(ambient), diffuse(diffuse), specular(specular) { }

	void Render(Shader* shader, Camera* camera);
	void Use(Shader* shader);

	void SetPosition(glm::vec3 newPosition) { position = newPosition; }
	void SetColor(glm::vec3 newColor) { color = newColor; }

private:
	glm::vec3 position;
	glm::vec3 color;
	glm::vec3 ambient;
	glm::vec3 diffuse;
	glm::vec3 specular;
	Mesh* mesh;

	const float scale = 0.1f;
};
