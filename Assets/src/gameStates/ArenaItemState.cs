// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.12
// 

using Colyseus.Schema;

public partial class ArenaItemState : Schema {
	[Type(0, "ref", typeof(V3))]
	public V3 position = new V3();

	[Type(1, "string")]
	public string uID = default(string);

	[Type(2, "string")]
	public string type = default(string);

	[Type(3, "number")]
	public float width = default(float);

	[Type(4, "number")]
	public float height = default(float);

	[Type(5, "number")]
	public float price = default(float);

	[Type(6, "string")]
	public string owner = default(string);
}

