using UnityEngine;

public class UISoundController : MonoBehaviour
{

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip pauseGameSound;
    [SerializeField]
    private AudioClip playGameSound;
    [SerializeField]
    private AudioClip clickUISound;


    //method for each of the possible sounds when interacting with the ui
    public void PlayPauseSound()
    {
        audioSource.clip = pauseGameSound;
        audioSource.Play();
    }

    public void PlayPlaySound()
    {
        audioSource.clip = playGameSound;
        audioSource.Play();
    }

    public void PlayClickSound()
    {
        audioSource.clip = clickUISound;
        audioSource.Play();
    }

}
