// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 0.5.39
// 

using Colyseus.Schema;

public class MapRoomState : Schema {
	[Type(0, "map", typeof(MapSchema<MapInfo>))]
	public MapSchema<MapInfo> maps = new MapSchema<MapInfo>();
}

