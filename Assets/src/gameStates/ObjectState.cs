// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.39
// 

using Colyseus.Schema;

public class ObjectState : Schema {
	[Type(0, "ref", typeof(V3))]
	public V3 position = new V3();

	[Type(1, "ref", typeof(Quat))]
	public Quat quaternion = new Quat();

	[Type(2, "string")]
	public string type = "";

	[Type(3, "ref", typeof(UserState))]
	public UserState owner = new UserState();

	[Type(4, "string")]
	public string uID = "";

	[Type(5, "boolean")]
	public bool instantiate = false;

	[Type(6, "string")]
	public string material = "";

	[Type(7, "ref", typeof(SoundState))]
	public SoundState sound = new SoundState();

	[Type(8, "number")]
	public float mass = 0;

	[Type(9, "string")]
	public string mesh = "";
}

