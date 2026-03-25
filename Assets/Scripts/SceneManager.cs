using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField] SoundManager soundManager;
    string _place = "HubScene";
    public string Place {  get { return _place; } }
    public void Teleport(string place)
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(_place);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(place, LoadSceneMode.Additive);
        _place = place;
        soundManager.ChangeSounds(_place);
    }
    public void Reload()
    {

    }
    void Start()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_place, LoadSceneMode.Additive);
    }
}
