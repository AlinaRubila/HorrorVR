using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Lock : MonoBehaviour
{
    SanityManager _sanityManager;
    [SerializeField] GameObject door;
    private void Awake()
    {
        _sanityManager = GameObject.FindWithTag("SanityManager").GetComponent<SanityManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RightKey"))
        {
            Rigidbody rb = door.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false;
            other.gameObject.GetComponent<Key>().FixateKey(transform);
            _sanityManager.ChangeValue(5);
            Debug.Log("Unlocking");
            GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
