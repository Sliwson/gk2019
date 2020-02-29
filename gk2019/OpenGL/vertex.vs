R""(
	#version 460 core
    layout (location = 0) in vec3 position;
    layout (location = 1) in vec3 normal;
    layout (location = 2) in vec2 texture;

    out vec3 fragmentPosition;
    out vec3 normalVector;
    out vec2 textureCoordinates;

    uniform mat4 model;
    uniform mat4 view;
    uniform mat4 projection;
    uniform mat3 normalMatrix;

    void main()
    {
       gl_Position = projection * view * model * vec4(position, 1.0);
       fragmentPosition = vec3(model * vec4(position, 1.0));
       normalVector = normalMatrix * normal;
       textureCoordinates = texture;
    }
)""