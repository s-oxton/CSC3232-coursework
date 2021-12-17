using UnityEngine;

public class SpinningBladeMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private Vector3 positionOne;
    [SerializeField]
    private Vector3 positionTwo;
    private Vector3 direction;

    [SerializeField]
    private float bladeSpeed;
    [SerializeField]
    private float bladeIdleTime;
    [SerializeField]
    private float bladeMovementTime;
    private float timer;
    private bool bladeMoving;

    public bool GetBladeMoving()
    {
        return bladeMoving;
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = bladeIdleTime;
        bladeMoving = false;
        direction = (positionTwo - positionOne).normalized;
    }

    private void FixedUpdate()
    {
        if (bladeMoving)
        {
            if (timer > 0)
            {
                //move the blade
                rb.velocity = direction * bladeSpeed * Time.fixedDeltaTime;
                //reduce the timer
                timer -= Time.fixedDeltaTime;
            }
            else
            {
                //switch movement bool
                bladeMoving = false;
                //setup new timer
                timer = bladeIdleTime;
                //switch target of blade
                direction *= -1;
                rb.velocity = new Vector3(0, 0, 0);
            }
        }
        else
        {
            if (timer > 0)
            {
                //do nothing
                //reduce the timer
                timer -= Time.fixedDeltaTime;
            }
            else
            {
                //switch movement bool
                bladeMoving = true;
                //setup timer
                timer = bladeMovementTime;
            }
        }
    }
}
