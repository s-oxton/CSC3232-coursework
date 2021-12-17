using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRaycast : MonoBehaviour
{

    [SerializeField]
    private float viewDistance;
    [SerializeField]
    private float attackDistance;
    [SerializeField]
    private LayerMask playerLayer;

    private RaycastHit2D hit;
    private Ray2D ray;

    public float GetViewDistance()
    {
        return viewDistance;
    }

    // Start is called before the first frame update
    void Start()
    {
        ray = new Ray2D();
    }

    public bool CheckRay(Transform rayOrigin, Vector3 rayDirection, string type)
    {

        bool rayStatus = false;
        ray.origin = rayOrigin.position;
        ray.direction = rayDirection;

        if (type == "visibility")
        {
            rayStatus = CastRay(viewDistance, "Player");
        }
        else if (type == "attackable")
        {
            rayStatus = CastRay(attackDistance, "Player");
        }

        return rayStatus;

    }

    private bool CastRay(float rayLength, string tag)
    {
        //fire the ray, put the hit details in hit
        hit = Physics2D.Raycast(ray.origin, ray.direction, rayLength, playerLayer);

        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.green);

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
