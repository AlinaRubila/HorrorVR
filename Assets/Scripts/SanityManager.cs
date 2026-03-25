using UnityEngine;

public class SanityManager : MonoBehaviour
{
    [SerializeField] PlayerState playerState;
    private void Update()
    {
        
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
