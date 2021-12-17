
using UnityEngine;

public class AxeSound : MonoBehaviour
{

    [SerializeField]
    private AxeController axeController;
    [SerializeField]
    private float maxHearingDistance;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip swing;
    [SerializeField]
    private float swingDelay;

    private void SwingSound()
    {
        audioSource.clip = swing;
        audioSource.Play();
    }

    public void DelayedSwingSound()
    {
        Invoke("SwingSound", swingDelay);
    }

    // Update is called once per frame
    void Update()
    {
        //update the volume of the sound depending on the distance to the player.
        float distanceToPlayer = axeController.GetDistanceToPlayer();
        //if within hearing distance
        if (distanceToPlayer <= maxHearingDistance && distanceToPlayer > 1f)
        {
            audioSource.volume = Mathf.Lerp(0, 1, 10 / distanceToPlayer);
        }
        else if (distanceToPlayer > maxHearingDistance)
        {
            audioSource.volume = 0;
        }
    }
}
