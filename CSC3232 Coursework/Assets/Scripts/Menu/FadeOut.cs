using System.Collections;
using UnityEngine;

public class FadeOut : MonoBehaviour
{

    [SerializeField]
    private float fadeRate;

    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void FadeStart()
    {
        StartCoroutine("FadeToBlack");
    }

    private IEnumerator FadeToBlack()
    {
        Color fadeOutColor = sprite.color;
        float fadeAmount;

        //while the alpha is less than 1
        while (fadeOutColor.a < 1)
        {
            //find the new alpha amount of the sprite
            fadeAmount = fadeOutColor.a + Mathf.Lerp(fadeOutColor.a, 1, fadeRate * Time.deltaTime);
            //set the new colour of the sprite
            fadeOutColor = new Color(fadeOutColor.r, fadeOutColor.g, fadeOutColor.b, fadeAmount);
            //update the sprite
            sprite.color = fadeOutColor;
            yield return null;
            Debug.Log("a");
        }

    }

}
