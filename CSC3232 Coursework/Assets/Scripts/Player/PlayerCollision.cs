using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    

    [SerializeField]
    private PlayerCombat playerCombat;
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private CircleCollider2D groundCollider;
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float immunityTime;
    private float timer;

    private bool grounded;

    private void Update()
    {
        RaycastHit2D hit;
        Ray2D ray = new Ray2D();

        //set the origin of the ray
        Vector2 positionVector = new Vector2(groundCollider.transform.position.x, groundCollider.transform.position.y + groundCollider.bounds.extents.y);
        ray.origin = positionVector;
        //set the direction of the ray
        ray.direction = Vector3.down;
        //set the length of the ray
        float rayLength = groundCollider.bounds.extents.y + 0.1f;

        //fire the ray, and put the details in the hit var
        hit = Physics2D.Raycast(ray.origin, ray.direction, rayLength, layerMask);

        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

        //if the ray hit something, and the thing it hit is the ground
        if (hit.collider != null && hit.transform.tag == "Ground")
        {
            //if the thing it hit is the ground
            //the player is grounded
            grounded = true;
        }
        else
        {
            //if the ray hits nothing, player is not grounded
            grounded = false;
        }

        if (timer > -1)
        {
            timer -= Time.deltaTime;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if u run into an enemy u take damage
        if (collision.gameObject.tag == "Enemy" && timer < 0)
        {
            playerCombat.TakeDamage(1);
            //get the direction between the objects
            Vector3 direction = (this.transform.position - collision.transform.position).normalized;
            //add a small force to the player
            playerMovement.DamageBump(direction);
            timer = immunityTime;
        }
    }

    public bool GetGrounded()
    {
        return grounded;
    }

}
