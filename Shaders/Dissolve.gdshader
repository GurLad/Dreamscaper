shader_type canvas_item;
uniform sampler2D noise;
uniform float percent;
uniform vec3 baseColor : source_color;

void fragment() {
	COLOR.rgb = COLOR.rgb * (1.0 - percent) + percent * baseColor;
	COLOR.a *= max(0, texture(noise, UV).r + (1.0 - percent * 2.0));
}
