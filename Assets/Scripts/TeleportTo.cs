using UnityEngine;

public class TeleportTo : MonoBehaviour
{
    public void Teleportate(string place)
    {
        Debug.Log("Teleportate!");
        SceneManager sceneManager = GameObject.FindWithTag("SceneManager").GetComponent<SceneManager>();
        sceneManager.Teleport(place);
    }
}
