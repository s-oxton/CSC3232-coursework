using System.Collections;
using UnityEngine;

public class AxeController : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float rotationSpeed;

    private void Start()
    {
        //set centre of mass to be the pivot point
        rb.centerOfMass = new Vector3(0, 0, 0);
    }

    private void FixedUpdate()
    {
        //update the rotation of the object
        rb.AddTorque(rotationSpeed * -Mathf.Sign(rb.rotation) * Time.fixedDeltaTime);

    }


}
