using UnityEngine;

public interface ConnectedObject{
void onMessage(ObjectMessage m);
void sendMessageToRoom(string m);
void setState(ObjectState state);
}