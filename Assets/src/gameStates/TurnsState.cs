// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.39
// 

using Colyseus.Schema;

public class TurnsState : Schema {
	[Type(0, "map", typeof(MapSchema<TurnPlayerState>))]
	public MapSchema<TurnPlayerState> players = new MapSchema<TurnPlayerState>();

	[Type(1, "number")]
	public float turn = 0;

	[Type(2, "number")]
	public float timerToStart = 0;

	[Type(3, "number")]
	public float gemsPerTurn = 0;
}

