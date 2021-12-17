using UnityEngine;

public class AxeController : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private AxeSound axeSound;

    [SerializeField]
    private float rotationSpeed;
    private float previousRotation;
    private float currentRotation;

    private void Start()
    {
        //set centre of mass to be the pivot point
        rb.centerOfMass = new Vector3(0, 0, 0);
        currentRotation = Mathf.Sign(rb.rotation);
    }

    private void FixedUpdate()
    {
        previousRotation = currentRotation;
        //update the rotation of the object
        rb.AddTorque(rotationSpeed * -Mathf.Sign(rb.rotation) * Time.fixedDeltaTime);
        currentRotation = Mathf.Sign(rb.rotation);
        //play sound when rotation direction changes.
        if (currentRotation != previousRotation)
        {
            axeSound.DelayedSwingSound();
        }
    }


    //gets the distance to the player
    public float GetDistanceToPlayer()
    {
        float distance = 0;
        distance = Vector2.Distance(rb.position, GameObject.Find("Player").GetComponent<PlayerMovement>().GetPosition());
        return distance;
    }

}
