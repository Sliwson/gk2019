R""(
	#version 460 core
    layout (location = 0) in vec3 position;
    layout (location = 1) in vec3 normal;
    layout (location = 2) in vec2 textureCoordinates;

	struct Material
	{
		sampler2D diffuse;
		sampler2D specular;
		float shininess;
	};

	struct PointLight
	{
		vec3 position;
		vec3 color;

		vec3 ambient;
		vec3 diffuse;
		vec3 specular;

		float constant;
		float linear;
		float quadratic;
	};

	struct SpotLight
	{
		vec3 position;
		vec3 direction;
		float cutOff;
		float outerCutOff;
		vec3 color;

		vec3 ambient;
		vec3 diffuse;
		vec3 specular;

		float constant;
		float linear;
		float quadratic;
	};
	
	struct DirectionalLight
	{
		vec3 direction;
		vec3 color;

		vec3 ambient;
		vec3 diffuse;
		vec3 specular;

		float constant;
		float linear;
		float quadratic;
	};

	out vec4 fragmentColor;

    uniform mat4 model;
    uniform mat4 view;
    uniform mat4 projection;
    uniform mat3 normalMatrix;
	uniform bool blinnPhong;
	uniform bool useFog;
	uniform vec3 backgroundColor;
	uniform vec3 viewPosition;
	uniform Material material;
	uniform PointLight pointLight;
	uniform SpotLight spotLight;
	uniform DirectionalLight directionalLight;

	vec4 GetPhongColor(vec3 lightPosition, vec3 attenuationProps, vec3 lightAmbient, vec3 lightDiffuse, vec3 lightSpecular, vec3 color, vec3 fragmentPosition, vec3 normalVector)
	{
		float distance = length(lightPosition - fragmentPosition);
		float attenuation = 1.0 / (attenuationProps.x + attenuationProps.y * distance + attenuationProps.z * distance * distance);
		
		vec3 diffuseColor = vec3(texture(material.diffuse, textureCoordinates));
		vec3 specularColor = vec3(texture(material.specular, textureCoordinates));
		
		vec3 normal = normalize(normalVector);
		vec3 lightVector = normalize(lightPosition - fragmentPosition);  
		float diffuseWeight = max(dot(normal, lightVector), 0.0);
		
		vec3 viewDirection = normalize(viewPosition - fragmentPosition);
		
		float specularWeight = 0.f;
		if (blinnPhong)
		{
			vec3 halfwayDirection = normalize(lightVector + viewDirection);
			specularWeight = pow(max(dot(normal, halfwayDirection), 0.0), material.shininess);
		}
		else
		{
			vec3 reflectDirection = reflect(-lightVector, normal);
			specularWeight = pow(max(dot(viewDirection, reflectDirection), 0.0), material.shininess);
		}
		
		vec3 ambient = lightAmbient * diffuseColor * color;
		vec3 diffuse = lightDiffuse * diffuseColor * diffuseWeight * color;
		vec3 specular = lightSpecular * specularColor * specularWeight * color;

		return vec4((ambient + diffuse + specular) * attenuation, 1.0);
	}
	
	vec4 GetDirectionalLightColor(vec3 fragmentPosition, vec3 normalVector)
	{
		vec3 color = directionalLight.color;
		vec3 diffuseColor = vec3(texture(material.diffuse, textureCoordinates));
		vec3 specularColor = vec3(texture(material.specular, textureCoordinates));

		vec3 normal = normalize(normalVector);
		vec3 lightVector = directionalLight.direction;
		float diffuseWeight = max(dot(normal, lightVector), 0.0);
		
		vec3 viewDirection = normalize(viewPosition - fragmentPosition);

		float specularWeight = 0.f;
		if (blinnPhong)
		{
			vec3 halfwayDirection = normalize(lightVector + viewDirection);
			specularWeight = pow(max(dot(normal, halfwayDirection), 0.0), material.shininess);
		}
		else
		{
			vec3 reflectDirection = reflect(-lightVector, normal);
			specularWeight = pow(max(dot(viewDirection, reflectDirection), 0.0), material.shininess);
		}

		vec3 ambient = directionalLight.ambient * diffuseColor * color;
		vec3 diffuse = directionalLight.diffuse * diffuseColor * diffuseWeight * color;
		vec3 specular = directionalLight.specular * specularColor * specularWeight * color;
		
		return vec4((ambient + diffuse + specular), 1.0);
	}

	vec4 GetPointLightColor(vec3 fragmentPosition, vec3 normalVector)
	{
		return GetPhongColor(pointLight.position, vec3(pointLight.constant, pointLight.linear, pointLight.quadratic), pointLight.ambient, pointLight.diffuse, pointLight.specular, pointLight.color, fragmentPosition, normalVector);
	}

	vec4 GetSpotLightColor(vec3 fragmentPosition, vec3 normalVector)
	{
		vec3 lightVector = normalize(spotLight.position - fragmentPosition);  
		float t = dot(lightVector, normalize(-spotLight.direction));
		float epsilon   = spotLight.cutOff - spotLight.outerCutOff;
		float intensity = clamp((t - spotLight.outerCutOff) / epsilon, 0.0, 1.0);   

		return GetPhongColor(spotLight.position, vec3(spotLight.constant, spotLight.linear, spotLight.quadratic), spotLight.ambient, spotLight.diffuse, spotLight.specular, spotLight.color, fragmentPosition, normalVector) * intensity;
	}

	void main()
	{
		gl_Position = projection * view * model * vec4(position, 1.0);
		vec3 fragmentPosition = vec3(model * vec4(position, 1.0));
		vec3 normalVector = normalMatrix * normal;

		fragmentColor = GetDirectionalLightColor(fragmentPosition, normalVector) + GetPointLightColor(fragmentPosition, normalVector) + GetSpotLightColor(fragmentPosition, normalVector);
		
		if (useFog)
		{
			float viewDistance = length(viewPosition - fragmentPosition);
			float fogFactor = 1.0 / exp(viewDistance * 0.25);
			fogFactor = clamp( fogFactor, 0.0, 1.0 );
			fragmentColor = vec4(mix(backgroundColor, fragmentColor.xyz, fogFactor), 1.0);
		}
    }
)""