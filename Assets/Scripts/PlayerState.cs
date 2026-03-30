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
        if (value == 100) _sanity = 100;
        else if (value > 0) _sanity += value * (_difficulty - 0.5f) * 0.1f;
        else _sanity += value * _difficulty * 0.1f;
        Debug.Log(_sanity);
    }
    public void ChangeDiffuculty(int level) 
    {
        _difficulty = level;
    }
}
