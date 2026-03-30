using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Key : MonoBehaviour
{
    SanityManager _sanityManager;
    private void Awake()
    {
        _sanityManager = GameObject.FindWithTag("SanityManager").GetComponent<SanityManager>();
    }
    public void IsFound() 
    {
        _sanityManager.ChangeValue(1);
    }
    public void FixateKey(Transform t)
    {
        GetComponent<XRGrabInteractable>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.SetParent(t);

    }
}
