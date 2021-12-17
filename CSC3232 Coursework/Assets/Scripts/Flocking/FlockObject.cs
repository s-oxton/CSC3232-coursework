using System.Collections.Generic;
using UnityEngine;

public class FlockObject : MonoBehaviour
{

    [SerializeField]
    private CircleCollider2D flockCollider;

    public CircleCollider2D GetCollider()
    {
        return flockCollider;
    }

    [SerializeField]
    private float speed;

    //returns a list of all the nearby flock objects
    public List<Transform> GetNearbyFlockObjects(float radius)
    {
        //initialises list
        List<Transform> nearbyObjects = new List<Transform>();
        //returns a list of all the  colliders of the flock objects that are nearby
        Collider2D[] nearbyColliders = Physics2D.OverlapCircleAll(transform.position, radius);

        //for each collider that overlapped
        foreach(Collider2D collider in nearbyColliders)
        {
            //if it's not the collider of this object
            if (collider != flockCollider)
            {
                //add its transform to the list
                nearbyObjects.Add(collider.transform);
            }
        }

        return nearbyObjects;
    }

    //updates the position of the flock object
    public void UpdatePosition(Vector2 move)
    {
        transform.up = move;
        transform.position += new Vector3(move.x, move.y, 0) * speed * Time.deltaTime;
    }

    


}
