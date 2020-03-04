#pragma once

#include <memory>
#include <string>
#include <glm/glm.hpp>
#include <glm/geometric.hpp>
#include <glm/gtc/matrix_transform.hpp>

class Mesh;
class Shader;
class Camera;

class Light
{
public:
	Light(Mesh* mesh, glm::vec3 color, glm::vec3 ambient, glm::vec3 diffuse, glm::vec3 specular) : 
		mesh(mesh), color(color), ambient(ambient), diffuse(diffuse), specular(specular) { }

	virtual void Render(Shader* shader, Camera* camera) = 0;
	virtual void Use(Shader* shader) = 0;

	void SetColor(glm::vec3 newColor) { color = newColor; }

protected:
	virtual void UseWithName(Shader* shader, std::string name);
	virtual void RenderWithPosition(Shader* shader, Camera* camera, const glm::vec3& position);

	glm::vec3 color;
	glm::vec3 ambient;
	glm::vec3 diffuse;
	glm::vec3 specular;

	float constant = 1.f;
	float linear = 0.09f;
	float quadratic = 0.032f;

	Mesh* mesh;

	const float scale = 0.1f;
};

class SpotLight : public Light
{
public:
	SpotLight(Mesh* mesh, glm::vec3 position, glm::vec3 color, glm::vec3 ambient, glm::vec3 diffuse, glm::vec3 specular, glm::vec3 direction, float cutoff) :
		Light(mesh, color, ambient, diffuse, specular), position(position), direction(direction), cutoff(cutoff) { }
	
	virtual void Render(Shader* shader, Camera* camera) override;
	virtual void Use(Shader* shader) override;

private:
	glm::vec3 position;
	glm::vec3 direction;
	float cutoff;
};

class PointLight : public Light
{
public:
	PointLight(Mesh* mesh, glm::vec3 position, glm::vec3 color, glm::vec3 ambient, glm::vec3 diffuse, glm::vec3 specular) :
		Light(mesh, color, ambient, diffuse, specular), position(position) { };

	virtual void Render(Shader* shader, Camera* camera) override;
	virtual void Use(Shader* shader) override;

	void SetPosition(glm::vec3 newPosition) { position = newPosition; }

private:
	glm::vec3 position;

};

//TODO:
class DirectionalLight : public Light
{
public:
	void Use(Shader* shader) override {}
	void Render(Shader* shader, Camera* camera) override {}
};
