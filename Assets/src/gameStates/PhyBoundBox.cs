// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.12
// 

using Colyseus.Schema;

public partial class PhyBoundBox : Schema {
	[Type(0, "ref", typeof(V3))]
	public V3 center = new V3();

	[Type(1, "ref", typeof(V3))]
	public V3 extents = new V3();
}

