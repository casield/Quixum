// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.12
// 

using Colyseus.Schema;

public partial class MoveMessage : Schema {
	[Type(0, "string")]
	public string uID = default(string);

	[Type(1, "number")]
	public float x = default(float);

	[Type(2, "number")]
	public float y = default(float);
}

