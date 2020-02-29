#include "light.h"
#include "shader.h"
#include "mesh.h"

void Light::Render(Shader* shader, const glm::mat4& view, const glm::mat4& projection)
{
	glm::mat4 model(1.f);
	model = glm::translate(model, position);
	model = glm::scale(model, glm::vec3(scale));

	shader->Use();
	shader->SetVector3("color", color);
	mesh->Draw(shader, model, view, projection);
}
