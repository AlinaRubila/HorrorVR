using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityManager : MonoBehaviour
{
    [SerializeField] PlayerState playerState;
    WaitForSeconds wait = new WaitForSeconds(1f);
    Dictionary<string, float> multipliers = new Dictionary<string, float>() { { "HubScene", 0 }, { "MazeScene", 0.1f }, { "HallScene", 0.3f }, { "GhostScene", 0.5f } };
    float currentMultiplier = 0;
    Coroutine sanityCoroutine;
    private void Start()
    {
        sanityCoroutine = StartCoroutine(DecreaseSanityRoutine());
    }
    private void OnDisable()
    {
        if (sanityCoroutine != null)
        StopCoroutine(sanityCoroutine);
    }

    IEnumerator DecreaseSanityRoutine()
    {
        while (playerState.Sanity > 0)
        {
            yield return wait;
            if (currentMultiplier != 0)
            ChangeValue(-currentMultiplier);
        }
    }
    public void ChangeMultiplier(string place)
    {
        currentMultiplier = multipliers[place];
    }
    public void ChangeValue(float value)
    {
        if (playerState.Difficulty != 3)
        playerState.ChangeSanity(value);
    }
    public float GetSanity()
    {
        return playerState.Sanity;
    }
}
