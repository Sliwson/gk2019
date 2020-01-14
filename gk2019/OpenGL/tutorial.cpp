#include "tutorial.h"
#include <string>
#include <iostream>

GLint CompileShader(GLenum type, const std::string& source, unsigned int shaderProgram)
{
    GLint shader = glCreateShader(type);

    const char* src = source.c_str();
    glShaderSource(shader, 1, &src, nullptr);
    glCompileShader(shader);

    GLint success;
    glGetShaderiv(shader, GL_COMPILE_STATUS, &success);
   
    if (!success)
    {
		char infoLog[512];
        glGetShaderInfoLog(shader, 512, NULL, infoLog);
        std::cout << "Error: " << infoLog << std::endl;
    }

    glAttachShader(shaderProgram, shader);
    return shader;
}

void CompileShaders()
{
    const std::string vertexShaderSource =
#include "vertex.vs"
	;

    const std::string pixelShaderSource =
#include "pixel.vs"
	;

    auto shaderProgram = glCreateProgram();
    auto vertexShader = CompileShader(GL_VERTEX_SHADER, vertexShaderSource, shaderProgram);
    auto pixelShader = CompileShader(GL_FRAGMENT_SHADER, pixelShaderSource, shaderProgram);

    glLinkProgram(shaderProgram);
    glUseProgram(shaderProgram);

    glDeleteShader(vertexShader);
    glDeleteShader(pixelShader);
}

void Clear()
{
	glClearColor(0.2f, 0.3f, 0.3f, 1.0f);
	glClear(GL_COLOR_BUFFER_BIT);
}

GLint GetTriangleVao()
{
    float vertices[] = {
     0.5f,  0.5f, 0.0f,  // top right
     0.5f, -0.5f, 0.0f,  // bottom right
    -0.5f, -0.5f, 0.0f,  // bottom left
    -0.5f,  0.5f, 0.0f   // top left 
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
    glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 3 * sizeof(float), nullptr);
    glEnableVertexAttribArray(0);
    
    unsigned int EBO;
    glGenBuffers(1, &EBO);
    
    glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBO);
    glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(indices), indices, GL_STATIC_DRAW);

    return VAO;
}

