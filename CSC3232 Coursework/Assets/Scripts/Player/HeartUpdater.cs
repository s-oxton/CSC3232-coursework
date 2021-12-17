using UnityEngine;
using UnityEngine.UI;

public class HeartUpdater : MonoBehaviour
{
    [SerializeField]
    private Sprite fullHeart;
    [SerializeField]
    private Sprite emptyHeart;

    private Image displayImage;

    private void Start()
    {
        displayImage = GetComponent<Image>();
    }

    public void SetHeartEmpty()
    {
        displayImage.sprite = emptyHeart;
    }

    public void SetHeartFull()
    {
        displayImage.sprite = fullHeart;
    }

}
