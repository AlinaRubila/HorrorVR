using UnityEngine;

public class Portal : MonoBehaviour
{
    SceneManager sceneManager;
    [SerializeField] string destination;
    private void Awake()
    {
        sceneManager = GameObject.FindWithTag("SceneManager").GetComponent<SceneManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactor"))
        {
            sceneManager.Teleport(destination);
        }
    }
}
