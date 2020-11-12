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
        if (m.message == "trigger_ShootAnim")
        {

            animator.SetTrigger("Shooting");
            Character.Instance.canRotate = true;
        }
    }
    public void setState(ObjectState state)
    {
        Debug.Log("SessionID " + state.owner.sessionId);
        if (state.owner.sessionId == Client.Instance.room.SessionId)
        {
            Debug.Log("Is me");
            Character.Instance.ballPointer = ballPointer;
        }
        else
        {
            ballPointer = GetComponentInChildren<BallPointer>();
            Destroy(ballPointer.gameObject);
        }
    }
}