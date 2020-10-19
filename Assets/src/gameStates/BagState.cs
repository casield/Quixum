// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.39
// 

using Colyseus.Schema;

public class BagState : Schema {
	[Type(0, "array", typeof(ArraySchema<PowerState>))]
	public ArraySchema<PowerState> slots = new ArraySchema<PowerState>();

	[Type(1, "array", typeof(ArraySchema<PowerState>))]
	public ArraySchema<PowerState> shop = new ArraySchema<PowerState>();

	[Type(2, "ref", typeof(UserState))]
	public UserState owner = new UserState();
}

