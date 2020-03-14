#pragma once

#include <assimp/Importer.hpp>
#include <assimp/scene.h>
#include <assimp/postprocess.h>

#include "texture.h"
#include "mesh.h"
#include "camera.h"
#include "vertex.h"

class Model
{
public:
    Model(std::string const& path);

    virtual void Render(Shader* shader, Camera* camera, glm::mat4 model);

protected:
    std::vector<std::unique_ptr<Texture>> textures;
    std::vector<std::unique_ptr<Mesh>> meshes;

    void LoadModel(std::string const& path);
    void ProcessNode(aiNode* node, const aiScene* scene);
    Mesh* ProcessMesh(aiMesh* mesh, const aiScene* scene);
    Texture* LoadMaterialTexture(aiMaterial* mat, aiTextureType type);
};
    