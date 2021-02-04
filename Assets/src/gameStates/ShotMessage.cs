// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.39
// 

using Colyseus.Schema;

public class ShotMessage : Schema {
	[Type(0, "number")]
	public float force = 0;

	[Type(1, "string")]
	public string client = "";

	[Type(2, "string")]
	public string room = "";
}

