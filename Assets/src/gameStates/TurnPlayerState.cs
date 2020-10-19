// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.39
// 

using Colyseus.Schema;

public class TurnPlayerState : Schema {
	[Type(0, "number")]
	public float gems = 0;

	[Type(1, "number")]
	public float initialShots = 0;

	[Type(2, "number")]
	public float shots = 0;

	[Type(3, "boolean")]
	public bool ballisMoving = false;

	[Type(4, "ref", typeof(V3))]
	public V3 checkpoint = new V3();

	[Type(5, "ref", typeof(UserState))]
	public UserState user = new UserState();

	[Type(6, "ref", typeof(BagState))]
	public BagState bag = new BagState();
}

