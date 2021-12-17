using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    [Header("Audio Sources")]
    [SerializeField]
    private AudioSource soundSource;
    [SerializeField]
    private AudioSource voiceSource;
    [SerializeField]
    private AudioSource skidSource;

    [Header("Character Audio")]
    [SerializeField]
    private AudioClip[] jump;
    [SerializeField]
    private AudioClip[] attack;
    [SerializeField]
    private AudioClip[] hurt;

    [Header("Movement Audio")]
    [SerializeField]
    private AudioClip footStep;
    [SerializeField]
    private AudioClip skid;
    [SerializeField]
    private AudioClip landJump;

    [Header("Combat Audio")]
    [SerializeField]
    private AudioClip[] swoosh;

    public void PlayFootStep()
    {
        soundSource.clip = footStep;
        soundSource.Play();
    }

    public void PlaySkid()
    {
        skidSource.clip = skid;
        //skidSource.Play();
    }

    public void PlayLandJump()
    {
        soundSource.clip = landJump;
        soundSource.Play();
    }

    public void PlayJump()
    {
        int audioNumber = Random.Range(0, 3);
        voiceSource.clip = jump[audioNumber];
        voiceSource.Play();
    }

    public void PlayAttack()
    {
        int audioNumber = Random.Range(0, 3);
        voiceSource.clip = attack[audioNumber];
        voiceSource.Play();
    }

    public void PlayHurt()
    {
        int audioNumber = Random.Range(0, 3);
        voiceSource.clip = hurt[audioNumber];
        voiceSource.Play();
    }

    public void PlaySwoosh()
    {
        int audioNumber = Random.Range(0, 5);
        soundSource.clip = swoosh[audioNumber];
        soundSource.Play();
    }

}
