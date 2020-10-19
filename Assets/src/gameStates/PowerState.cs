// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.39
// 

using Colyseus.Schema;

public class PowerState : Schema {
	[Type(0, "map", typeof(MapSchema<UserState>))]
	public MapSchema<UserState> listUsers = new MapSchema<UserState>();

	[Type(1, "number")]
	public float duration = 0;

	[Type(2, "number")]
	public float cost = 0;

	[Type(3, "string")]
	public string UIName = "";

	[Type(4, "string")]
	public string UIDesc = "";

	[Type(5, "boolean")]
	public bool active = false;

	[Type(6, "string")]
	public string uID = "";

	[Type(7, "string")]
	public string type = "";
}

