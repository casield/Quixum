// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.39
// 

using Colyseus.Schema;

public class Message : Schema {
	[Type(0, "ref", typeof(UserState))]
	public UserState user = new UserState();

	[Type(1, "string")]
	public string message = "";
}

