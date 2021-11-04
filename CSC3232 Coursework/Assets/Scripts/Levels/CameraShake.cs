using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    [SerializeField]
    private float shakeDuration;
    [SerializeField]
    private float magnitude;

    public IEnumerator ShakeCamera(int strength)
    {
        //get the original position of the camera
        Vector3 originalPos = transform.position;
        //set the duration to be the shake duration
        float duration = shakeDuration;

        //while the duration is above 0
        while (duration > 0)
        {
            //set new values for the x and y positions
            float x = Random.Range(-magnitude, magnitude) * strength;
            float y = Random.Range(-magnitude, magnitude) * strength;
            //update camera position
            transform.localPosition = new Vector3(x, y, originalPos.z);

            duration -= Time.deltaTime;

            yield return null;
        }
    }

}
