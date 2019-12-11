#version 330 core

in vec2 outUV;
out vec3 color;
in vec3 Normal;
in vec3 FragPos;

uniform sampler2D myTextureSampler;
uniform vec3 lightPos;
uniform vec3 lightColor;

void main(){
	vec3 norm = normalize(Normal);
	vec3 lightDir = normalize(lightPos - FragPos);

	float diff = max(dot(norm, lightDir), 0.0);
	vec3 diffuse = diff * lightColor;

	float ambientStrength = 0.1;
    vec3 ambient = ambientStrength * lightColor;

	vec3 objectColor = texture( myTextureSampler, outUV ).rgb;

	vec3 result = (ambient + diffuse) * objectColor;
	color = result;
}