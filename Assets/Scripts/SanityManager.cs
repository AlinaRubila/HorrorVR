using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityManager : MonoBehaviour
{
    [SerializeField] PlayerState playerState;
    WaitForSeconds wait = new WaitForSeconds(1f);
    Dictionary<string, float> multipliers = new Dictionary<string, float>() { { "HubScene", 0}, { "MazeScene", 0.01f }, {"HallScene", 0.03f}, {"GhostScene", 0.05f} };
    float currentMultiplier = 0;
    private void Start()
    {
        StartCoroutine(DecreaseSanityRoutine());
    }
    private void OnDisable()
    {
        StopCoroutine(DecreaseSanityRoutine());
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
        playerState.ChangeSanity(value);
    }
    public int CheckStatus()
    {
        if (playerState.Sanity == 0) return 0;
        else return 1;
    }
}
