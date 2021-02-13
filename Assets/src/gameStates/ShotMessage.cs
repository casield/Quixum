// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.12
// 

using Colyseus.Schema;

public partial class ShotMessage : Schema {
	[Type(0, "number")]
	public float force = default(float);

	[Type(1, "string")]
	public string client = default(string);

	[Type(2, "string")]
	public string room = default(string);
}

