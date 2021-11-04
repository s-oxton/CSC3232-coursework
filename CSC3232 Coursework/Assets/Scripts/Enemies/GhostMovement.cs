using UnityEngine;

public class GhostMovement : MonoBehaviour
{

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float yOffset;

    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float turnSpeed;

    private bool playerHit;

    private Vector2 direction;

    public void SetPlayerHit(bool temp)
    {
        playerHit = temp;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 offsetPosition = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);

        //get the direction to go
        //if player is to the right
        if (playerTransform.transform.position.x > offsetPosition.x)
        {
            direction.x = 1;
        }
        else
        {
            direction.x = -1;
        }

        //if player is above
        if (playerTransform.transform.position.y > offsetPosition.y)
        {
            direction.y = 1;
        }
        else
        {
            direction.y = -1;
        }

        //the the x velocity sign is opposite to the sprite direction, flip it
        if (Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            //flip the sprite
            Vector3 tempVector = transform.localScale;
            if (rb.velocity.x > 0f)
            {
                tempVector.x = Mathf.Abs(tempVector.x);
            }
            else if (rb.velocity.x < 0f)
            {
                tempVector.x = -Mathf.Abs(tempVector.x);
            }
            transform.localScale = tempVector;
        }

    }

    private void FixedUpdate()
    {
        if (!playerHit)
        {
            float targetSpeed;
            float difference;
            float movementForce;

            //if the ghost is going in the right y direction
            if (direction.y * rb.velocity.y < 0f)
            {
                //add friction force
                rb.AddForce(direction.y * Vector2.up * turnSpeed, ForceMode2D.Impulse);

            }
            else
            {
                //calculate the movement force
                targetSpeed = direction.y * maxSpeed;
                difference = targetSpeed - rb.velocity.y;
                movementForce = Mathf.Pow(Mathf.Abs(difference), acceleration) * Mathf.Sign(difference);
                //add the force
                if (Mathf.Abs(movementForce) > 0.05f)
                {
                    rb.AddForce(movementForce * Vector2.up * Time.fixedDeltaTime);

                }
            }

            //if the ghost is going in the right x direction
            if (direction.x * rb.velocity.x < 0f)
            {
                //add friction force
                rb.AddForce(Mathf.Sign(direction.x) * Vector2.right * turnSpeed, ForceMode2D.Impulse);
            }
            else
            {

                //calculate the movement force
                targetSpeed = direction.x * maxSpeed;
                difference = targetSpeed - rb.velocity.x;
                movementForce = Mathf.Pow(Mathf.Abs(difference), acceleration) * Mathf.Sign(difference);
                //add the force
                if (Mathf.Abs(movementForce) > 0.05f)
                {
                    rb.AddForce(movementForce * Vector2.right * Time.fixedDeltaTime);
                }
            }
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            rb.isKinematic = true;
        }





    }





}
