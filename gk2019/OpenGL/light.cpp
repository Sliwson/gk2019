#include "light.h"
#include "shader.h"
#include "mesh.h"
#include "camera.h"

void Light::UseWithName(Shader* shader, std::string name)
{
	shader->Use();
	shader->SetVector3(name + ".color", color);
	shader->SetVector3(name + ".ambient", ambient);
	shader->SetVector3(name + ".diffuse", diffuse);
	shader->SetVector3(name + ".specular", specular);
	shader->SetFloat(name + ".constant", constant);
	shader->SetFloat(name + ".linear", linear);
	shader->SetFloat(name + ".quadratic", quadratic);

}

void Light::RenderWithPosition(Shader* shader, Camera* camera, const glm::vec3& position)
{
	glm::mat4 model(1.f);
	model = glm::translate(model, position);
	model = glm::scale(model, glm::vec3(scale));

	shader->Use();
	shader->SetVector3("color", color);
	mesh->Draw(shader, camera, model);
}

void SpotLight::Render(Shader* shader, Camera* camera)
{
	//do not render directional light cube
	return;
}

void SpotLight::Use(Shader* shader)
{
	UseWithName(shader, "spotLight");
	shader->SetVector3("spotLight.position", position);
	shader->SetVector3("spotLight.direction", direction);
	shader->SetFloat("spotLight.cutOff", glm::cos(cutoff));
	shader->SetFloat("spotLight.outerCutOff", glm::cos(1.5f * cutoff));
}

void PointLight::Render(Shader* shader, Camera* camera)
{
	RenderWithPosition(shader, camera, position);
}

void PointLight::Use(Shader* shader)
{
	UseWithName(shader, "pointLight");
	shader->SetVector3("pointLight.position", position);
}
