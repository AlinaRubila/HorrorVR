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
    public string Place {  get { return _place; } }
    public void Teleport(string place)
    {
        StartCoroutine(LoadAndTeleport(place));
    }
    IEnumerator LoadAndTeleport(string place)
    {
        yield return StartCoroutine(fader.FadeOut());
        yield return UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(_place);
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(place, LoadSceneMode.Additive);
        yield return new WaitForEndOfFrame();
        Scene newScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(place);
        UnityEngine.SceneManagement.SceneManager.SetActiveScene(newScene);
        GameObject spawnPoint = GameObject.FindWithTag("SpawnPoint");
        if (spawnPoint != null)
        {
            //CharacterController cc = player.GetComponent<CharacterController>();
            //Debug.Log(cc == null);
            /*if (cc != null)
                cc.enabled = false;*/
            Vector3 pos = spawnPoint.transform.position;
            /*pos.y += cc.height / 2f;*/
            player.position = pos;
           /* if (cc != null)
                cc.enabled = true;*/
        }
        _place = place;
        soundManager.ChangeSounds(_place);
        sanityManager.ChangeValue(100);
        sanityManager.ChangeMultiplier(place);
        yield return StartCoroutine(fader.FadeIn());
    }
    void Start()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_place, LoadSceneMode.Additive);
    }
}
