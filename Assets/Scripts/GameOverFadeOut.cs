using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverFadeOut : MonoBehaviour
{
    void Update()
    {
        // Fade out the image in 3 seconds
        GetComponent<UnityEngine.UI.Image>().CrossFadeAlpha(0, 3, false);
    }
}
