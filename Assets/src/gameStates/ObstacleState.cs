// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.39
// 

using Colyseus.Schema;

public class ObstacleState : Schema {
	[Type(0, "string")]
	public string uID = "";

	[Type(1, "string")]
	public string status = "";

	[Type(2, "string")]
	public string objectname = "";

	[Type(3, "ref", typeof(V3))]
	public V3 position = new V3();

	[Type(4, "ref", typeof(Quat))]
	public Quat quaternion = new Quat();

	[Type(5, "array", typeof(ArraySchema<V3>))]
	public ArraySchema<V3> extraPoints = new ArraySchema<V3>();
}

