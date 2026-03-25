using UnityEngine;

public class Key : MonoBehaviour
{
    bool _isRight;
    SanityManager _sanityManager;
    [SerializeField] GameObject door;
    private void Awake()
    {
        _sanityManager = GameObject.FindWithTag("SanityManager").GetComponent<SanityManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!_isRight) return;
        if (collision.gameObject.CompareTag("Lock"))
        {
            Rigidbody rb = door.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false;
        }
    }
}
