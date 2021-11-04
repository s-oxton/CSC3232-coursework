using UnityEngine;

public class EnemyRaycast : MonoBehaviour
{

    [SerializeField]
    private DumbMovement enemyMovement;
    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float visionDistance;
    [SerializeField]
    private float closeDistance;

    private RaycastHit2D hit;
    private Ray2D ray;
    private bool playerVisible;
    private bool playerAttackable;

    public bool GetPlayerVisisble()
    {
        return playerVisible;
    }

    public bool GetPlayerAttackable()
    {
        return playerAttackable;
    }

    private void Start()
    {
        ray = new Ray2D();
    }

    // Update is called once per frame
    void Update()
    {
        //get the origin of the ray
        ray.origin = gameObject.transform.position;
        //get the direction the enemy is facing
        ray.direction = gameObject.transform.TransformDirection(Vector2.right * enemyMovement.GetDirection());

        //if the enemy can see the player
        playerVisible = CastRay(visionDistance, playerLayer, "Player");
        //if the enemy can attack the player
        playerAttackable = CastRay(closeDistance, playerLayer, "Player");

    }

    private bool CastRay(float rayLength, LayerMask layer, string tag)
    {
        //fire the ray, put the hit details in hit
        hit = Physics2D.Raycast(ray.origin, ray.direction, rayLength, layer); ;

        //Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

        //if the ray hits the player box collider
        if (hit.collider != null && hit.transform.tag == tag && hit.collider is BoxCollider2D)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
