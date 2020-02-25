#include "tutorial.h"
#include <string>
#include <iostream>

Shader* CreateShader()
{
    const std::string vertexShaderSource =
#include "vertex.vs"
	;

    const std::string pixelShaderSource =
#include "pixel.ps"
	;

    return new Shader(vertexShaderSource, pixelShaderSource);
}

void Clear()
{
	glClearColor(0.2f, 0.3f, 0.3f, 1.0f);
	glClear(GL_COLOR_BUFFER_BIT);
}

GLint GetTriangleVao()
{
    float vertices[] = {
     0.5f,  0.5f, 0.0f,   1.f, 0.f, 0.f,  1.0f, 1.0f, // top right
     0.5f, -0.5f, 0.0f,   0.f, 1.f, 0.f,  1.0f, 0.0f, // bottom right
    -0.5f, -0.5f, 0.0f,   0.f, 0.f, 1.f,  0.0f, 0.0f, // bottom left
    -0.5f,  0.5f, 0.0f,   .5f, .5f, .5f,  0.0f, 1.0f  // top left 
    };

    unsigned int indices[] = {
        0, 1, 3,   // first triangle
        1, 2, 3    // second triangle
    };

    unsigned int VAO;
    glGenVertexArrays(1, &VAO);
    glBindVertexArray(VAO);
	
    unsigned int VBO;
    glGenBuffers(1, &VBO);
    
    glBindBuffer(GL_ARRAY_BUFFER, VBO);
    glBufferData(GL_ARRAY_BUFFER, sizeof(vertices), vertices, GL_STATIC_DRAW);

    const auto stride = 8 * sizeof(float);
    glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, stride, nullptr);
    glEnableVertexAttribArray(0);
    glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, stride, (void*)(3 * sizeof(float)));
    glEnableVertexAttribArray(1);
    glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, stride, (void*)(6 * sizeof(float)));
    glEnableVertexAttribArray(2);
    
    unsigned int EBO;
    glGenBuffers(1, &EBO);
    
    glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBO);
    glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(indices), indices, GL_STATIC_DRAW);

    return VAO;
}

