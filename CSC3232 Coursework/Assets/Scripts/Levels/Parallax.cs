using UnityEngine;

public class Parallax : MonoBehaviour
{

    [Header("References")]

    [SerializeField]
    private CameraMovement cameraMovement;

    [SerializeField]
    [Range(0, 1)]
    private float parallaxValue;
    [SerializeField]
    private float repeatDistance;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3();

        float temp = cameraMovement.transform.position.x * (1 - parallaxValue);

        float parallaxTransform = cameraMovement.transform.position.x * parallaxValue;

        newPosition = new Vector3(startPosition.x + parallaxTransform, startPosition.y, startPosition.z);

        transform.position = newPosition;

        if (temp > startPosition.x + repeatDistance)
        {
            startPosition.x += repeatDistance;
        }
        else if (temp < startPosition.x - repeatDistance)
        {
            startPosition.x -= repeatDistance;
        }

    }
}
