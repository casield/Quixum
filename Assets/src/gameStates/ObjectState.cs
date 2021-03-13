// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.12
// 

using Colyseus.Schema;

public partial class ObjectState : Schema {
	[Type(0, "ref", typeof(V3))]
	public V3 position = new V3();

	[Type(1, "ref", typeof(Quat))]
	public Quat quaternion = new Quat();

	[Type(2, "string")]
	public string type = default(string);

	[Type(3, "string")]
	public string owner = default(string);

	[Type(4, "string")]
	public string uID = default(string);

	[Type(5, "boolean")]
	public bool instantiate = default(bool);

	[Type(6, "string")]
	public string material = default(string);

	[Type(7, "number")]
	public float mass = default(float);

	[Type(8, "string")]
	public string mesh = default(string);
}

