using UnityEngine;

public class FlyingSounds : MonoBehaviour
{

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private FlyingMonsterMovement enemyMovement;
    [SerializeField]
    private float maxHearingDistance;

    [SerializeField]
    private AudioClip flapSound;
    [SerializeField]
    private AudioClip attackSound;
    [SerializeField]
    private AudioClip damageSound;


    public void PlayFlap()
    {
        audioSource.clip = flapSound;
        audioSource.Play();
    }

    public void PlayAttack()
    {
        audioSource.clip = attackSound;
        audioSource.Play();
    }

    public void PlayTakeDamage()
    {
        audioSource.clip = damageSound;
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
