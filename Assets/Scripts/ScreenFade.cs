using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    public Image fadeImage;
    public IEnumerator FadeOut()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 2;
            fadeImage.color = new Color(1, 1, 1, t);
            yield return null;
        }
    }
    public IEnumerator FadeIn()
    {
        float t = 1;
        while (t > 0)
        {
            t -= Time.deltaTime * 2;
            fadeImage.color = new Color(1, 1, 1, t);
            yield return null;
        }
    }
}
