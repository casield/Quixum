// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.39
// 

using Colyseus.Schema;

public class TurnsState : Schema {
	[Type(0, "number")]
	public float turn = 0;

	[Type(1, "number")]
	public float phase = 0;

	[Type(2, "map", typeof(MapSchema<UserState>))]
	public MapSchema<UserState> ready = new MapSchema<UserState>();
}

