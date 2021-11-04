using UnityEngine;
using UnityEngine.UI;

public class UpdateLevelPin : MonoBehaviour
{

    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Image image;


    public void UpdatePin()
    {
        //update the sprite
        spriteRenderer.sprite = image.sprite;
        //set the alpha to max
        spriteRenderer.color = new Color(1f,1f,1f,1f);
    }

}
