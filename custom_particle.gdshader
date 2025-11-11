shader_type canvas_item;

// Uniforms for shader customization
uniform float wave_intensity = 0.1;
uniform float wave_frequency = 10.0;
uniform vec4 color_start : source_color = vec4(1.0, 0.5, 0.0, 1.0);
uniform vec4 color_end : source_color = vec4(1.0, 0.0, 0.5, 1.0);

void fragment() {
	// Get the current UV coordinates
	vec2 uv = UV;
	
	// Apply wave distortion to X based on Y position and time
	uv.x += sin(uv.y * wave_frequency + TIME) * wave_intensity;
	
	// Sample the texture with distorted UVs
	vec4 texture_color = texture(TEXTURE, uv);
	
	// Create color gradient based on UV.y (bottom to top)
	vec4 gradient_color = mix(color_start, color_end, UV.y);
	
	// Combine texture with gradient
	COLOR = texture_color * gradient_color;
}
