using UnityEngine;

public class SmokeAnimator : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private PlayerMovement playerMovement;
    private bool playerRunning;

    void Start()
    {
        playerRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if method is only true the first frame the player's velocity gets above 10
        if (!playerRunning && Mathf.Abs(playerMovement.GetXVelocity()) > 10f)
        {
            //trigger the animation
            animator.SetTrigger("FastRun");
            //enable the animation to be true
            spriteRenderer.enabled = true;
            //set the player to be running
            playerRunning = true;
        } else if (Mathf.Abs(playerMovement.GetXVelocity()) < 10f)
            //if the player ever goes below 10 speed, they are no longer running
        {
            playerRunning = false;
        }
    }

    public void EndAnimation()
    {
        spriteRenderer.enabled = false;
    }

}
