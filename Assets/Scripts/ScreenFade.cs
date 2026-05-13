using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    [SerializeField] Volume globalVolume;
    ColorAdjustments colorAdjustments;
    private void Awake()
    {
        globalVolume.profile.TryGet(out colorAdjustments);
    }
    public IEnumerator FadeOut()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 2f;
            colorAdjustments.postExposure.value = Mathf.Lerp(0f, -10f, t);
            yield return null;
        }
        colorAdjustments.postExposure.value = -10f;
        Debug.Log("FadeOut");
    }
    public IEnumerator FadeIn()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 2;
            colorAdjustments.postExposure.value = Mathf.Lerp(-10, 0f, t);
            yield return null;
        }
        colorAdjustments.postExposure.value = 0f;
        Debug.Log("FadeIn");
    }
    public IEnumerator FadeWhite()
    {
        Camera cam = Camera.main;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / 3;
            colorAdjustments.postExposure.value = Mathf.Lerp(0, 10f, t);
            cam.backgroundColor = Color.Lerp(Color.black, Color.white, t);
            yield return null;
        }
    }
}
