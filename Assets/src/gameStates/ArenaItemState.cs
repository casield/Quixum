// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.39
// 

using Colyseus.Schema;

public class ArenaItemState : Schema {
	[Type(0, "ref", typeof(V3))]
	public V3 position = new V3();

	[Type(1, "string")]
	public string uID = "";

	[Type(2, "string")]
	public string type = "";

	[Type(3, "number")]
	public float width = 0;

	[Type(4, "number")]
	public float height = 0;

	[Type(5, "number")]
	public float price = 0;

	[Type(6, "string")]
	public string owner = "";
}

