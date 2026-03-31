using UnityEngine;

public class PlayerState : MonoBehaviour
{
    float _sanity = 50f;
    int _difficulty = 1;
    float distance = 0f;
    [SerializeField] Vector3 previousPosition;
    [SerializeField] Transform player;
    [SerializeField] SoundManager _soundManager;
    [SerializeField] SceneManager _sceneManager;
    [SerializeField] GBManager _GBManager;
    public float Sanity { get => _sanity; }
    public int Difficulty { get => _difficulty; }
    public void Update()
    {
        distance += Vector3.Distance(player.position, previousPosition);
        previousPosition = player.position;
        if (distance >= 1)
        {
            _soundManager.ManageFootsteps(1);
            distance = 0;
        }
    }
    public void ChangeSanity(float value)
    {
        float a = 0;
        if (value == 100)
        {
            _sanity = 50;
            _soundManager.ChangeEffects(100);
            _GBManager.ChangeEffects(100);
        }
        else if (value > 0)
        {
            a = value * (_difficulty - 0.5f) * 0.1f;
            _sanity += a;
            _soundManager.ChangeEffects(a * 2);
            _GBManager.ChangeEffects(a * 2);
        }
        else 
        { 
            a = value * _difficulty * 0.1f;
            _sanity += a;
            _soundManager.ChangeEffects(a);
            _GBManager.ChangeEffects(a);
        }
        if (_sanity <= 0)
        {
            _sceneManager.Teleport("HubScene");
        }
    }
    public void ChangeDiffuculty(int level) 
    {
        _difficulty = level;
    }
    
}
