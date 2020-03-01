#pragma once

#include "mesh.h"
#include "vertex.h"
#include <numeric>

Material GetSimpleMaterial()
{
	Material m;
	m.ambient = { 1.f, .5f, .3f };
	m.diffuse = { 1.f, .5f, .3f };
	m.specular = { .5f, .5f, .5f };
	m.shininess = 32.f;
	return m;
}

Mesh* GetTriangleMesh()
{
	std::vector<Vertex> vertices{ {
	{ {0.5f,  0.5f, 0.0f}, {0.f, 0.f, 0.f}, {1.0f, 1.0f}}, // top right
	{ {0.5f,  -0.5f, 0.0f}, {0.f, 0.f, 0.f}, {1.0f, 0.0f}}, // top right
	{ {-0.5f, -0.5f, 0.0f}, {0.f, 0.f, 0.f}, { .0f, .0f}}, // top right
	{ {-0.5f,  0.5f, 0.0f}, {0.f, 0.f, 0.f}, { .0f, 1.0f}}, // top right
	} };

	std::vector<unsigned int> indices{
		0, 1, 3,   // first triangle
		1, 2, 3    // second triangle
	};

	return new Mesh(vertices, indices, GetSimpleMaterial());
}

Mesh* GetCubeMesh()
{
	std::vector<Vertex> vertices{ {
	{{-0.5f, -0.5f, -0.5f}, {0.0f,  0.0f, -1.0f}, {0.0f, 0.0f}},
	{{ 0.5f, -0.5f, -0.5f}, {0.0f,  0.0f, -1.0f}, {1.0f, 0.0f}},
	{{ 0.5f,  0.5f, -0.5f}, {0.0f,  0.0f, -1.0f}, {1.0f, 1.0f}},
	{{ 0.5f,  0.5f, -0.5f}, {0.0f,  0.0f, -1.0f}, {1.0f, 1.0f}},
	{{-0.5f,  0.5f, -0.5f}, {0.0f,  0.0f, -1.0f}, {0.0f, 1.0f}},
	{{-0.5f, -0.5f, -0.5f}, {0.0f,  0.0f, -1.0f}, {0.0f, 0.0f}},
								  
	{{-0.5f, -0.5f,  0.5f}, {0.0f,  0.0f, 1.0f},  {0.0f, 0.0f}},
	{{ 0.5f, -0.5f,  0.5f}, {0.0f,  0.0f, 1.0f},  {1.0f, 0.0f}},
	{{ 0.5f,  0.5f,  0.5f}, {0.0f,  0.0f, 1.0f},  {1.0f, 1.0f}},
	{{ 0.5f,  0.5f,  0.5f}, {0.0f,  0.0f, 1.0f},  {1.0f, 1.0f}},
	{{-0.5f,  0.5f,  0.5f}, {0.0f,  0.0f, 1.0f},  {0.0f, 1.0f}},
	{{-0.5f, -0.5f,  0.5f}, {0.0f,  0.0f, 1.0f},  {0.0f, 0.0f}},
											
	{{-0.5f,  0.5f,  0.5f}, {-1.0f,  0.0f,  0.0f}, {1.0f, 0.0f}},
	{{-0.5f,  0.5f, -0.5f}, {-1.0f,  0.0f,  0.0f}, {1.0f, 1.0f}},
	{{-0.5f, -0.5f, -0.5f}, {-1.0f,  0.0f,  0.0f}, {0.0f, 1.0f}},
	{{-0.5f, -0.5f, -0.5f}, {-1.0f,  0.0f,  0.0f}, {0.0f, 1.0f}},
	{{-0.5f, -0.5f,  0.5f}, {-1.0f,  0.0f,  0.0f}, {0.0f, 0.0f}},
	{{-0.5f,  0.5f,  0.5f}, {-1.0f,  0.0f,  0.0f}, {1.0f, 0.0f}},
																				   
	{{ 0.5f,  0.5f,  0.5f}, {1.0f,  0.0f,  0.0f}, {1.0f, 0.0f}},
	{{ 0.5f,  0.5f, -0.5f}, {1.0f,  0.0f,  0.0f}, {1.0f, 1.0f}},
	{{ 0.5f, -0.5f, -0.5f}, {1.0f,  0.0f,  0.0f}, {0.0f, 1.0f}},
	{{ 0.5f, -0.5f, -0.5f}, {1.0f,  0.0f,  0.0f}, {0.0f, 1.0f}},
	{{ 0.5f, -0.5f,  0.5f}, {1.0f,  0.0f,  0.0f}, {0.0f, 0.0f}},
	{{ 0.5f,  0.5f,  0.5f}, {1.0f,  0.0f,  0.0f}, {1.0f, 0.0f}},
							   
	{{-0.5f, -0.5f, -0.5f}, {0.0f, -1.0f,  0.0f}, {0.0f, 1.0f}},
	{{ 0.5f, -0.5f, -0.5f}, {0.0f, -1.0f,  0.0f}, {1.0f, 1.0f}},
	{{ 0.5f, -0.5f,  0.5f}, {0.0f, -1.0f,  0.0f}, {1.0f, 0.0f}},
	{{ 0.5f, -0.5f,  0.5f}, {0.0f, -1.0f,  0.0f}, {1.0f, 0.0f}},
	{{-0.5f, -0.5f,  0.5f}, {0.0f, -1.0f,  0.0f}, {0.0f, 0.0f}},
	{{-0.5f, -0.5f, -0.5f}, {0.0f, -1.0f,  0.0f}, {0.0f, 1.0f}},
							   
	{{-0.5f,  0.5f, -0.5f}, {0.0f,  1.0f,  0.0f}, {0.0f, 1.0f}},
	{{ 0.5f,  0.5f, -0.5f}, {0.0f,  1.0f,  0.0f}, {1.0f, 1.0f}},
	{{ 0.5f,  0.5f,  0.5f}, {0.0f,  1.0f,  0.0f}, {1.0f, 0.0f}},
	{{ 0.5f,  0.5f,  0.5f}, {0.0f,  1.0f,  0.0f}, {1.0f, 0.0f}},
	{{-0.5f,  0.5f,  0.5f}, {0.0f,  1.0f,  0.0f}, {0.0f, 0.0f}},
	{{-0.5f,  0.5f, -0.5f}, {0.0f,  1.0f,  0.0f}, {0.0f, 1.0f}}
	}};                                         

	std::vector<unsigned int> indices(vertices.size());
	std::iota(std::begin(indices), std::end(indices), 0);

	return new Mesh(vertices, indices, GetSimpleMaterial());
}

