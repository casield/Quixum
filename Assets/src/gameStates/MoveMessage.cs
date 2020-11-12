// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.39
// 

using Colyseus.Schema;

public class MoveMessage : Schema {
	[Type(0, "string")]
	public string uID = "";

	[Type(1, "number")]
	public float x = 0;

	[Type(2, "number")]
	public float y = 0;

	[Type(3, "number")]
	public float rotX = 0;

	[Type(4, "number")]
	public float rotZ = 0;
}

