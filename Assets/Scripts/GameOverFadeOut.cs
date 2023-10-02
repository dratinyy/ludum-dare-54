using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverFadeOut : MonoBehaviour
{
    public UnityEngine.UI.Image img;
    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime/2)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            print("fade in");
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime/2)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
    }
    void Start()
    {
        img = GetComponent<UnityEngine.UI.Image>();
        StartCoroutine(FadeImage(false));
    }
}
