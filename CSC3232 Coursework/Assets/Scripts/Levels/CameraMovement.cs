using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    #region Camera Settings
    [Header("References")]
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Rigidbody2D playerRigidbody;
    [Space(10)]

    [Header("Camera Settings")]
    [SerializeField]
    private Vector2 minBounds;
    [SerializeField]
    private Vector2 maxBounds;
    [Space(10)]

    [Header("X Axis")]
    [SerializeField]
    [Range(0, 10)]
    private float minXSmoothing;
    [SerializeField]
    [Range(0, 10)]
    private float maxXSmoothing;
    [SerializeField]
    [Range(0, 10)]
    private float xSmoothingMultiple;
    [Space(5)]
    [SerializeField]
    [Range(0, 3)]
    private float xViewMultiple;
    [SerializeField]
    [Range(1, 20)]
    private float maxXOffset;
    [Space(5)]

    [Header("Y Axis")]
    [SerializeField]
    [Range(0, 50)]
    private float ySmoothing;
    [SerializeField]
    [Range(0, 5)]
    private float constantYOffset;
    [SerializeField]
    [Range(0.01f, 1f)]
    private float yOffsetMultiple;
    [Space(5)]

    [Header("Z Axis")]
    [SerializeField]
    private float zOffset;
    #endregion

    private bool isStatic;

    public void UpdateTargetTransform(Transform newTransform, bool tempIsStatic)
    {
        playerTransform = newTransform;
        isStatic = tempIsStatic;
    }

    private void Start()
    {
        isStatic = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        Vector3 targetPosition;
        Vector3 boundedPosition;
        Vector3 playerPosition;

        //get position of player
        playerPosition = GetPlayerPositionVector();

        float xOffset = 0;
        float yOffset = 0;

        if (!isStatic)
        {
            //offsets of the camera
            xOffset = CalculateXOffset(playerPosition);
            yOffset = CalculateYOffset(playerPosition);
        } else
        {
            //temp settings for when the cameea is being locked to the boss room
            xOffset = playerTransform.position.x;
            yOffset = -2.3f;
        }

        //smoothing amount of the camera on the x axis
        float xSmoothing = CalculateXSmoothing(playerPosition, xOffset);

        //get the new target position of the camera
        targetPosition = GetTargetPosition(xOffset, playerPosition.y + yOffset + constantYOffset, xSmoothing);

        //ensure the camera is not out of bounds
        boundedPosition = BoundCameraPosition(targetPosition);

        //set the new camera position
        transform.position = boundedPosition;

    }

    private float CalculateXSmoothing(Vector3 playerPosition, float xOffset)
    {
        //the further away from the player the target position is, the higher the smoothing value
        //stops the player catching up with the camera when it is far away.
        float xSmoothing;
        //gets the difference of the x position of the player and the camera, and then multiplies it by a set value and the player's velocity, and bounds it between two values
        xSmoothing = Mathf.Clamp(Mathf.Abs(xOffset - playerPosition.x) * xSmoothingMultiple * Mathf.Abs(playerRigidbody.velocity.x), minXSmoothing, maxXSmoothing);
        return xSmoothing;

    }

    private Vector3 GetTargetPosition(float xOffset, float yOffset, float xSmoothing)
    {
        Vector3 targetPosition = new Vector3();
        // calculate where the position of the camera should be, depending on the offset and the smoothing amount
        targetPosition.x = Mathf.Lerp(transform.position.x, xOffset, xSmoothing * Time.deltaTime);
        targetPosition.y = Mathf.Lerp(transform.position.y, yOffset, ySmoothing * Time.deltaTime);
        targetPosition.z = zOffset;

        return targetPosition;
    }

    private Vector3 GetPlayerPositionVector()
    {
        Vector3 playerPosition = new Vector3();
        //set the vector's variables to be equal to the player position
        playerPosition = playerTransform.position;
        return playerPosition;
    }

    private float CalculateXOffset(Vector3 playerPosition)
    {
        float xOffset;

        //calculate the position of the camera depending on the player's velocity, and the settings of the camera
        //x Offset can never be more than the max X offset setting
        xOffset = playerPosition.x + Mathf.Min(maxXOffset, Mathf.Pow(Mathf.Abs(playerRigidbody.velocity.x), xViewMultiple)) * Mathf.Sign(playerRigidbody.velocity.x);

        return xOffset;
    }

    private float CalculateYOffset(Vector3 playerPosition)
    {
        float yOffset;
        //calculates the camera offset depending on the player's height
        yOffset = -playerPosition.y * yOffsetMultiple;

        return yOffset;
    }

    private Vector3 BoundCameraPosition(Vector3 targetPosition)
    {
        //clamps the x and y values to ensure the camera stays within the bounds of the map
        return new Vector3(
            Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y),
            targetPosition.z);
    }

}
