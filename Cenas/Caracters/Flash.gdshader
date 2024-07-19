shader_type canvas_item;
uniform bool hit;
void fragment() {
	vec4 input_color = texture(TEXTURE, UV);
	if (hit == true) 
	{
	COLOR = vec4(0.85,0.85,0.85,input_color.a);
	}
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
