#include "model.h"

Model::Model(std::string const& path)
{
	LoadModel(path);
}

void Model::Render(Shader* shader, Camera* camera, glm::mat4 model)
{
	for (auto& m : meshes)
		m->Draw(shader, camera, model);
}

void Model::LoadModel(std::string const& path)
{
	Assimp::Importer importer;
	const aiScene* scene = importer.ReadFile(path, aiProcess_Triangulate | aiProcess_FlipUVs | aiProcess_CalcTangentSpace);

	if (!scene || scene->mFlags & AI_SCENE_FLAGS_INCOMPLETE || !scene->mRootNode)
	{
		std::cout << "ERROR::ASSIMP:: " << importer.GetErrorString() << std::endl;
		return;
	}

	ProcessNode(scene->mRootNode, scene);
}

void Model::ProcessNode(aiNode* node, const aiScene* scene)
{
	for (unsigned int i = 0; i < node->mNumMeshes; i++)
	{
		aiMesh* mesh = scene->mMeshes[node->mMeshes[i]];
		meshes.push_back(std::unique_ptr<Mesh>(ProcessMesh(mesh, scene)));
	}

	for (unsigned int i = 0; i < node->mNumChildren; i++)
	{
		ProcessNode(node->mChildren[i], scene);
	}
}

Mesh* Model::ProcessMesh(aiMesh* mesh, const aiScene* scene)
{
	std::vector<Vertex> vertices;
	std::vector<unsigned int> indices;
	std::vector<Texture*> textures;

	for (unsigned int i = 0; i < mesh->mNumVertices; i++)
	{
		Vertex vertex;
		glm::vec3 vector;

		vector.x = mesh->mVertices[i].x;
		vector.y = mesh->mVertices[i].y;
		vector.z = mesh->mVertices[i].z;
		vertex.Position = vector;

		vector.x = mesh->mNormals[i].x;
		vector.y = mesh->mNormals[i].y;
		vector.z = mesh->mNormals[i].z;
		vertex.Normal = vector;

		if (mesh->mTextureCoords[0])
		{
			glm::vec2 vec;
			vec.x = mesh->mTextureCoords[0][i].x;
			vec.y = mesh->mTextureCoords[0][i].y;
			vertex.TexCoords = vec;
		}
		else
			vertex.TexCoords = glm::vec2(0.0f, 0.0f);

		vertices.push_back(vertex);
	}

	for (unsigned int i = 0; i < mesh->mNumFaces; i++)
	{
		aiFace face = mesh->mFaces[i];
		for (unsigned int j = 0; j < face.mNumIndices; j++)
			indices.push_back(face.mIndices[j]);
	}
	
	aiMaterial* material = scene->mMaterials[mesh->mMaterialIndex];

	Material* myMaterial = new Material {
		42.f,
		std::unique_ptr<Texture>(LoadMaterialTexture(material, aiTextureType_DIFFUSE)),
		std::unique_ptr<Texture>(LoadMaterialTexture(material, aiTextureType_SPECULAR))
	};

	return new Mesh(vertices, indices, myMaterial);
}

Texture* Model::LoadMaterialTexture(aiMaterial* mat, aiTextureType type)
{
	if (mat->GetTextureCount(type) < 1)
		return nullptr;

	aiString str;
	mat->GetTexture(type, 0, &str);
		
	Texture* texture = new Texture(str.C_Str(), type == aiTextureType_DIFFUSE ? TextureType::Diffuse : TextureType::Specular );
	return texture;
}
