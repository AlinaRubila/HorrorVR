using UnityEngine;
using UnityEngine.Audio;

public class Door : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] HingeJoint hinge;
    float minVelocity = 0.5f;
    float velocity;
    private void Update()
    {
        velocity = Mathf.Abs(hinge.velocity);
        if (velocity > minVelocity)
        {
            if (!source.isPlaying) source.Play();
            source.volume = Mathf.Clamp01(velocity / 100f);
            source.pitch = 0.8f + velocity / 200f;
        }
        else
        {
            if (source.isPlaying) source.Stop();
        }
    }

}
