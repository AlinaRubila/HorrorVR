using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GBManager : MonoBehaviour
{
    [SerializeField] Volume globalVolume;
    ColorAdjustments colorAdjustments;
    Vignette vignette;
    private void Start()
    {
        globalVolume.profile.TryGet(out colorAdjustments);
        globalVolume.profile.TryGet(out vignette);
    }
    public void SetVignette(float value)
    {
        if (vignette == null) return;
        vignette.intensity.value = value;
    }
    public void ChangeEffects(float value)
    {
        if (colorAdjustments != null)
        {

            if (value == 100)
            {
                colorAdjustments.contrast.value = 0f;
                colorAdjustments.saturation.value = 0f;
            }
            else
            {
                float currentContract = Mathf.Clamp(colorAdjustments.contrast.value - (value), 0f, 20f);
                float currentSaturation = Mathf.Clamp(colorAdjustments.saturation.value + (value), -40f, 0f);
                colorAdjustments.contrast.value = currentContract;
                colorAdjustments.saturation.value = currentSaturation;
            }
        }
    }
}
