// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.39
// 

using Colyseus.Schema;

public class GameState : Schema {
	[Type(0, "ref", typeof(WorldState))]
	public WorldState world = new WorldState();

	[Type(1, "map", typeof(MapSchema<UserState>))]
	public MapSchema<UserState> users = new MapSchema<UserState>();

	[Type(2, "ref", typeof(UserState))]
	public UserState winner = new UserState();

	[Type(3, "string")]
	public string name = "";

	[Type(4, "string")]
	public string mapName = "";

	[Type(5, "ref", typeof(TurnsState))]
	public TurnsState turnState = new TurnsState();

	[Type(6, "ref", typeof(ChatState))]
	public ChatState chat = new ChatState();
}

