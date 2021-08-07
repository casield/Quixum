using UnityEngine;

public interface IConnectedObject{
void onMessage(ObjectMessage m);
void sendMessageToRoom(string m);
void setState(ObjectState state);
}

public class ConnectedObject : MonoBehaviour, IConnectedObject
{
    public ObjectState state;
    public virtual void onMessage(ObjectMessage m)
    {
       
    }

    public async virtual void sendMessageToRoom(string mess)
    {
        ObjectMessage obms = new ObjectMessage();
        obms.uID = this.state.uID;
        obms.room = Client.Instance.room.SessionId;
        obms.message = mess;
        await Client.Instance.room.Send("objectMessage", obms);
    }

    public virtual void setState(ObjectState state)
    {
        this.state = state;
    }
}