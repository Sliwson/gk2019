#include "mesh.h"
#include "shader.h"
#include "vertex.h"
#include "camera.h"

Mesh::Mesh(std::vector<Vertex> vertices, std::vector<GLuint> indices, Material* material)
{
    this->vertices = vertices;
    this->indices = indices;
    this->material.reset(material);

    Init();
}

void Mesh::Draw(Shader* shader, Camera* camera, const glm::mat4& model)
{
    shader->Use();
    shader->SetMatrix("model", model);
    shader->SetMatrix("projection", camera->GetProjectionMatrix());
    shader->SetMatrix("view", camera->GetViewMatrix());
    shader->SetVector3("viewPosition", camera->GetPosition());
	
    const auto normal = glm::mat3(transpose(inverse(model)));
    shader->SetMatrix3("normalMatrix", normal);

    shader->SetFloat("material.shininess", material->shininess); 
    if (material->diffuse != nullptr)
        material->diffuse->Use(shader);
    if (material->specular != nullptr)
        material->specular->Use(shader);

	glBindVertexArray(VAO);
	glDrawElements(GL_TRIANGLES, indices.size(), GL_UNSIGNED_INT, 0);
	glBindVertexArray(0);
}

Mesh::~Mesh()
{
    glDeleteBuffers(1, &EBO);
    glDeleteBuffers(1, &VBO);
    glDeleteVertexArrays(1, &VAO);
}

void Mesh::Init()
{
    glGenVertexArrays(1, &VAO);
    glGenBuffers(1, &VBO);
    glGenBuffers(1, &EBO);

    glBindVertexArray(VAO);
    glBindBuffer(GL_ARRAY_BUFFER, VBO);

    glBufferData(GL_ARRAY_BUFFER, vertices.size() * sizeof(Vertex), &vertices[0], GL_STATIC_DRAW);

    glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBO);
    glBufferData(GL_ELEMENT_ARRAY_BUFFER, indices.size() * sizeof(unsigned int),
        &indices[0], GL_STATIC_DRAW);

    glEnableVertexAttribArray(0);
    glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, sizeof(Vertex), (void*)0);
    glEnableVertexAttribArray(1);
    glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, sizeof(Vertex), (void*)offsetof(Vertex, Normal));
    glEnableVertexAttribArray(2);
    glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, sizeof(Vertex), (void*)offsetof(Vertex, TexCoords));

    glBindVertexArray(0);
}
