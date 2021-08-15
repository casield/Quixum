// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.12
// 

using Colyseus.Schema;

public partial class UserState : Schema {
	[Type(0, "string")]
	public string sessionId = default(string);

	[Type(1, "string")]
	public string name = default(string);

	[Type(2, "number")]
	public float gems = default(float);

	[Type(3, "number")]
	public float energy = default(float);

	[Type(4, "number")]
	public float wins = default(float);

	[Type(5, "ref", typeof(Gauntlet))]
	public Gauntlet gauntlet = new Gauntlet();
}

