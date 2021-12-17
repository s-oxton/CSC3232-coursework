using System.Collections.Generic;
using UnityEngine;

public class FlockController : MonoBehaviour
{
    [SerializeField]
    private float[] weights;
    [SerializeField]
    private float smoothing;

    public Vector2 CalculateMovement(FlockObject flockObject, List<Transform> nearbyObjects, List<Transform> avoidObjects)
    {
        Vector2 movement = new Vector2(0, 0);
        Vector2 cohesionMove;
        Vector2 separationMove;
        Vector2 alignmentMove = flockObject.transform.up;

        //if there's objects nearby
        if (nearbyObjects.Count > 0)
        {
            //adjust the movement through each of the movement functions
            separationMove = Seperation(flockObject, avoidObjects) * weights[0];
            alignmentMove = Alignment(flockObject, nearbyObjects) * weights[1];
            cohesionMove = Cohesion(flockObject, nearbyObjects) * weights[2];

            movement = separationMove + alignmentMove + cohesionMove;
        }
        //if there's no objects nearby
        else
        {
            //can just move the flock object forwards
            movement = alignmentMove;
        }
        //return the movement
        return movement;
    }

    private Vector2 Seperation(FlockObject flockObject, List<Transform> avoidObjects)
    {
        Vector2 averagePosition = new Vector2(0, 0);

        if (avoidObjects.Count > 0)
        {
            //sum up all the transforms in the objects that are too close
            foreach (Transform transform in avoidObjects)
            {
                averagePosition -= new Vector2(transform.position.x, transform.position.y);
                averagePosition += new Vector2(flockObject.transform.position.x, flockObject.transform.position.y);
            }
            //get the average of them
            averagePosition /= avoidObjects.Count;

            //makes the position in local space instead of world space

        }


        return averagePosition;
    }

    //calculates the new alignment of the flock object
    private Vector2 Alignment(FlockObject flockObject, List<Transform> nearbyObjects)
    {
        Vector2 averageRotation = new Vector2(0, 0);

        //sum up all the rotations in the objects nearby
        foreach (Transform transform in nearbyObjects)
        {
            averageRotation += new Vector2(transform.up.x, transform.up.y);
        }
        //get the average of them
        averageRotation /= nearbyObjects.Count;

        return averageRotation;
    }

    //calculates the target transform of the nearby objects
    private Vector2 Cohesion(FlockObject flockObject, List<Transform> nearbyObjects)
    {
        Vector2 averagePosition = new Vector2(0, 0);

        //sum up all the transforms in the objects nearby
        foreach (Transform transform in nearbyObjects)
        {
            averagePosition += new Vector2(transform.position.x, transform.position.y);
        }
        //get the average of them
        averagePosition /= nearbyObjects.Count;

        //makes the position in local space instead of world space
        averagePosition -= new Vector2(flockObject.transform.position.x, flockObject.transform.position.y);
        //apply smoothing factor to reduce flickering
        //it doesn't work that well but it's better than before...
        float xTransform = Mathf.Lerp(flockObject.transform.up.x, averagePosition.x, smoothing);
        float yTransform = Mathf.Lerp(flockObject.transform.up.y, averagePosition.y, smoothing);
        averagePosition = new Vector2(xTransform, yTransform);

        return averagePosition;
    }

}
