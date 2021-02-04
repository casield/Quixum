using UnityEngine;

public class Player : MonoBehaviour, ConnectedObject
{
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    BallPointer ballPointer;
    Animator animator;

    void Start()
    {

        animator = GetComponent<Animator>();

        animator.SetTrigger("Hello");
    }
    public void onMessage(ObjectMessage m)
    {
        Debug.Log(m);
        if (m.message == "trigger_ShootAnim")
        {

            animator.SetTrigger("Shooting");
            Character.Instance.canRotate = true;
        }
        if (m.message == "Snap_true")
        {
            animator.SetBool("Snapped", true);
        }
        if (m.message == "Snap_false")
        {

            animator.SetBool("Snapped", false);
        }
    }
    public void setState(ObjectState state)
    {
        if (state.owner.sessionId == Client.Instance.room.SessionId)
        {
            Character.Instance.ballPointer = ballPointer;
            Character.Instance.player = this;
            CameraController.Instance.player = this;
        }
        else
        {
            ballPointer = GetComponentInChildren<BallPointer>();
            Destroy(ballPointer.gameObject);
        }
    }

    public void sendMessageToRoom(string m)
    {
        throw new System.NotImplementedException();
    }
}