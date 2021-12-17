using UnityEngine;

public class EnemySounds : MonoBehaviour
{

    [SerializeField]
    private DumbMovement enemyMovement;
    [SerializeField]
    private float maxHearingDistance;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip takeDamage;
    [SerializeField]
    private AudioClip bite;
    [SerializeField]
    private AudioClip footstep;

    public void TakeDamageSound()
    {
        audioSource.clip = takeDamage;
        audioSource.Play();
    }

    public void FootstepSound()
    {
        audioSource.clip = footstep;
        audioSource.Play();
    }

    public void BiteSound()
    {
        audioSource.clip = bite;
        audioSource.Play();
    }

    private void Update()
    {
        //update the volume of the sound depending on the distance to the player.
        float distanceToPlayer = enemyMovement.GetDistanceToPlayer();
        //if within hearing distance
        if (distanceToPlayer <= maxHearingDistance && distanceToPlayer > 1f)
        {
            audioSource.volume = Mathf.Lerp(0, 1, 5 / distanceToPlayer);
        }
        else if (distanceToPlayer > maxHearingDistance)
        {
            audioSource.volume = 0;
        }
    }

}
