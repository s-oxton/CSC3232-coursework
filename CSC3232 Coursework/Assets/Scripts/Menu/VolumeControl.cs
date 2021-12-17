using UnityEngine;

public class VolumeControl : MonoBehaviour
{

    [SerializeField]
    private AudioSource audioSource;

    //sets the volume to be the slider's volume
    public void UpdateVolume(float sliderLevel)
    {
        audioSource.volume = sliderLevel;
    }

    //pauses the music when the user pauses, and vice versa
    public void ChangeMusicPauseState(bool playMusic)
    {
        if (playMusic)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }

    }

}
