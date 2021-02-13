// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.12
// 

using Colyseus.Schema;

public partial class GameState : Schema {
	[Type(0, "ref", typeof(WorldState))]
	public WorldState world = new WorldState();

	[Type(1, "map", typeof(MapSchema<UserState>))]
	public MapSchema<UserState> users = new MapSchema<UserState>();

	[Type(2, "ref", typeof(UserState))]
	public UserState winner = new UserState();

	[Type(3, "string")]
	public string name = default(string);

	[Type(4, "string")]
	public string mapName = default(string);

	[Type(5, "ref", typeof(TurnsState))]
	public TurnsState turnState = new TurnsState();
}

