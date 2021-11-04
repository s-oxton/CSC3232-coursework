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

}
