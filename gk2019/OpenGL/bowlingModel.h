#pragma once

#include "model.h"

class BowlingModel : public Model
{
public:
    BowlingModel(const std::string& name) : Model(name) {}
    void Update(float dt);
    void Render(Shader* shader, Camera* camera, glm::mat4 model) override;

private:
    float time = 0.f;
};
