using UnityEngine;

public class Flashlight : MonoBehaviour
{
    SanityManager _sanityManager;
    [SerializeField] GhostTeleportation _ghost;
    private void Awake()
    {
        _sanityManager = GameObject.FindWithTag("SanityManager").GetComponent<SanityManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ghost"))
        {
            _ghost.Disappear();
            _sanityManager.ChangeValue(1);
        }
    }
}
