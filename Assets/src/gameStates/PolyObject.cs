// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.39
// 

using Colyseus.Schema;

public class PolyObject : ObjectState {
	[Type(10, "array", typeof(ArraySchema<V3>))]
	public ArraySchema<V3> verts = new ArraySchema<V3>();

	[Type(11, "array", typeof(ArraySchema<V3>))]
	public ArraySchema<V3> normals = new ArraySchema<V3>();

	[Type(12, "array", "number")]
	public ArraySchema<float> faces = new ArraySchema<float>();
}

