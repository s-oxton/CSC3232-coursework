using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{

    [SerializeField]
    private FlockController flockController;
    [SerializeField]
    private FlockObject flockObject;

    private List<FlockObject> flockList;

    [SerializeField]
    [Range(10,100)]
    private int flockCount;
    [SerializeField]
    private float viewDistance;
    [SerializeField]
    private float avoidDistance;

    [SerializeField]
    private float[] screenBoundaries;

    private void Start()
    {
        flockList = new List<FlockObject>();

        //instantiate all the flock objects
        for (int i = 0; i < flockCount; i++)
        {
            //generate a random position for the flock object
            Vector3 position = new Vector3(Random.Range(-9f, 9f), Random.Range(-5f, 5f), 0);
            //create the flock object
            FlockObject temp = Instantiate(flockObject, position, Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)), transform);
            flockList.Add(temp);
        }
    }

    private void Update()
    {

        Vector2 move;

        //for every flock object
        foreach (FlockObject flobject in flockList)
        {
            //gets the list of all the nearby object transforms
            List<Transform> nearbyObjects = flobject.GetNearbyFlockObjects(viewDistance);

            //gets the list of all the object transforms that are too close
            List<Transform> avoidObjects = flobject.GetNearbyFlockObjects(avoidDistance);

            //calculate the movement of the flock object
            move = flockController.CalculateMovement(flobject, nearbyObjects, avoidObjects);

            //move the flobject
            flobject.UpdatePosition(move);
            
            //move the flobject to the opposite side of the screen if it is outside the boundaries
            if (Mathf.Abs(flobject.transform.position.x) > screenBoundaries[0])
            {
                flobject.transform.position = new Vector2(-screenBoundaries[0] * Mathf.Sign(flobject.transform.position.x), flobject.transform.position.y);
            } else if (Mathf.Abs(flobject.transform.position.y) > screenBoundaries[1])
            {
                flobject.transform.position = new Vector2(flobject.transform.position.x, -screenBoundaries[1] * Mathf.Sign(flobject.transform.position.y));
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, viewDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, avoidDistance);
    }

}
