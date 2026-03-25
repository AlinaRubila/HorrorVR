using UnityEngine;

public class PlayerState : MonoBehaviour
{
    float _sanity = 100f;
    int _difficulty = 1;
    [SerializeField] SoundManager soundManager;
    [SerializeField] SceneManager sceneManager;
    [SerializeField] GBManager GBManager;
    public float Sanity { get => _sanity; }
    public int Difficulty { get => _difficulty; }
    public void ChangeSanity(float value)
    {
        _sanity += value;
    }
    public void ChangeDiffuculty(int level) 
    {
        _difficulty = level;
    }
}
