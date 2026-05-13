using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneManager : MonoBehaviour
{
    [SerializeField] SoundManager soundManager;
    [SerializeField] SanityManager sanityManager;
    [SerializeField] Transform player;
    [SerializeField] ScreenFade fader;
    string _place = "HubScene";
    bool isLoading = false;
    public string Place {  get { return _place; } }
    public void Teleport(string place)
    {
        if (isLoading) return;
        if (place == "Beyond") 
        {
            StartCoroutine(TeleportBeyond());
            return;
        }
        StartCoroutine(LoadAndTeleport(place));
        Debug.Log($"{_place}, {place}");
    }
    IEnumerator LoadAndTeleport(string place)
    {
        isLoading = true;
        yield return StartCoroutine(fader.FadeOut());
        yield return UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(_place);
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(place, LoadSceneMode.Additive);
        yield return new WaitForEndOfFrame();
        Scene newScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(place);
        UnityEngine.SceneManagement.SceneManager.SetActiveScene(newScene);
        GameObject spawnPoint = GameObject.FindWithTag("SpawnPoint");
        if (spawnPoint != null)
        {
            Vector3 pos = spawnPoint.transform.position;
            player.position = pos;
        }
        _place = place;
        soundManager.ChangeSounds(_place);
        sanityManager.ChangeValue(100);
        sanityManager.ChangeMultiplier(place);
        yield return StartCoroutine(fader.FadeIn());
        isLoading = false;
    }
    IEnumerator TeleportBeyond()
    {
        AudioSource source = GameObject.FindWithTag("Beyond").GetComponent<AudioSource>();
        source.Play();
        yield return StartCoroutine(fader.FadeWhite());
        yield return new WaitForSeconds(5f);
        source.Stop();
        Application.Quit();
        Debug.Log("Quit!");
    }
    IEnumerator Start()
    {
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_place, LoadSceneMode.Additive);
    }
}
