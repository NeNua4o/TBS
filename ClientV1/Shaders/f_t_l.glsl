#version 330 core

in vec2 outUV;
out vec4 color;
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

	float cosAlpha = clamp( dot(norm, lightDir), 0, 1 );
	float dist = distance (FragPos, lightPos);
	vec3 specular = lightColor * pow(cosAlpha,5) / (dist*dist);

	vec3 objectColor = texture( myTextureSampler, outUV ).rgb;

	vec3 result = (ambient + diffuse + specular) * objectColor;
	color = vec4(result, 0.5);
}