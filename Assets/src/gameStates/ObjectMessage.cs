// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.12
// 

using Colyseus.Schema;

public partial class ObjectMessage : Schema {
	[Type(0, "string")]
	public string uID = default(string);

	[Type(1, "string")]
	public string message = default(string);

	[Type(2, "string")]
	public string room = default(string);
}

