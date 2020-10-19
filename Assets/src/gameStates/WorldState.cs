// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.39
// 

using Colyseus.Schema;

public class WorldState : Schema {
	[Type(0, "map", typeof(MapSchema<ObjectState>))]
	public MapSchema<ObjectState> objects = new MapSchema<ObjectState>();

	[Type(1, "array", typeof(ArraySchema<ObjectState>))]
	public ArraySchema<ObjectState> tiles = new ArraySchema<ObjectState>();

	[Type(2, "array", typeof(ArraySchema<ObstacleState>))]
	public ArraySchema<ObstacleState> obstacles = new ArraySchema<ObstacleState>();
}

