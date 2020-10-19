// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.39
// 

using Colyseus.Schema;

public class MapInfo : Schema {
	[Type(0, "map", typeof(MapSchema<ObjectState>))]
	public MapSchema<ObjectState> objects = new MapSchema<ObjectState>();

	[Type(1, "string")]
	public string name = "";
}

