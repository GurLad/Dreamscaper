shader_type canvas_item;
uniform sampler2D noise;
uniform float percent;
uniform vec3 baseColor : source_color;

void fragment() {
	float p = 1.0 - percent;
	COLOR.rgb = COLOR.rgb * (1.0 - p) + p * baseColor;
	COLOR.a *= max(0, texture(noise, UV).r + (1.0 - p * 2.0));
}
