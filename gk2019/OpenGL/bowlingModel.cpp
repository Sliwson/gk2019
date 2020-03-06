#include "bowlingModel.h"
#include <glm/gtc/matrix_transform.hpp>

void BowlingModel::Update(float dt)
{
	time += dt;
}

void BowlingModel::Render(Shader* shader, Camera* camera, glm::mat4 model)
{
	auto sphere = meshes[1].get();
	auto pin = meshes[3].get();

	for (int y = 0; y < 3; y++)
		for (int x = 0; x < y + 1; x++)
			pin->Draw(shader, camera, glm::translate(glm::mat4(1.f), { x, 0.f, y }));

	const auto t = time / 2.f;
	glm::mat4 sphereModel = glm::translate(glm::mat4(1.0f), GetSpherePosition()) *
		glm::rotate(glm::mat4(1.0f), t, glm::vec3{ 0.5f, 1.f, 0.f });
	sphere->Draw(shader, camera, sphereModel);
}

glm::vec3 BowlingModel::GetSpherePosition()
{
	const auto radius = 3.f;
	const auto t = time / 2.f;
	return { glm::sin(t) * radius, 0, glm::cos(t) * radius };
}
