// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.12
// 

using Colyseus.Schema;

public partial class TurnsState : Schema {
	[Type(0, "string")]
	public string winner = default(string);

	[Type(1, "number")]
	public float turn = default(float);

	[Type(2, "number")]
	public float phase = default(float);

	[Type(3, "array", typeof(ArraySchema<string>), "string")]
	public ArraySchema<string> ready = new ArraySchema<string>();
}

