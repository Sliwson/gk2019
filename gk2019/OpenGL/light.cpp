#include "light.h"
#include "shader.h"
#include "mesh.h"
#include "camera.h"

void Light::Render(Shader* shader, Camera* camera)
{
	glm::mat4 model(1.f);
	model = glm::translate(model, position);
	model = glm::scale(model, glm::vec3(scale));

	shader->Use();
	shader->SetVector3("color", color);
	mesh->Draw(shader, camera, model);
}

void Light::Use(Shader* shader)
{
	shader->Use();
	shader->SetVector3("lightPosition", position);
	shader->SetVector3("lightColor", color);
}
