using UnityEngine;

public class FlyingMonsterCollision : MonoBehaviour
{

    private bool grounded;

    public bool GetGrounded()
    {
        return grounded;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }

}
