// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.39
// 

using Colyseus.Schema;

public class EulerQuat : Schema {
	[Type(0, "ref", typeof(Quat))]
	public Quat quat = new Quat();

	[Type(1, "ref", typeof(V3))]
	public V3 euler = new V3();
}

