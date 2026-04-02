using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GBManager : MonoBehaviour
{
    [SerializeField] Volume globalVolume;
    ChromaticAberration chromaticAberration;
    ColorAdjustments colorAdjustments;
    Vignette vignette;
    private void Start()
    {
        globalVolume.profile.TryGet(out chromaticAberration);
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
        if (chromaticAberration != null && colorAdjustments != null)
        {

            if (value == 100)
            {
                chromaticAberration.intensity.value = 0f;
                colorAdjustments.contrast.value = 0f;
                colorAdjustments.saturation.value = 0f;
            }
            else
            {
                float currentAb = Mathf.Clamp(chromaticAberration.intensity.value - (value * 0.01f), 0f, 0.2f);
                float currentContract = Mathf.Clamp(colorAdjustments.contrast.value - (value), 0f, 20f);
                float currentSaturation = Mathf.Clamp(colorAdjustments.saturation.value + (value), -40f, 0f);
                chromaticAberration.intensity.value = currentAb;
                colorAdjustments.contrast.value = currentContract;
                colorAdjustments.saturation.value = currentSaturation;
            }
        }
    }
}
