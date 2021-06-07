// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.12
// 

using Colyseus.Schema;

public partial class SwipeMessage : Schema {
	[Type(0, "number")]
	public float degree = default(float);

	[Type(1, "ref", typeof(V3))]
	public V3 direction = new V3();
}

