using UnityEngine;

public interface ConnectedObject{
void onMessage(ObjectMessage m);
void setState(ObjectState state);
}