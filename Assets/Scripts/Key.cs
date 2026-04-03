using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Key : MonoBehaviour
{
    SanityManager _sanityManager;
    bool firstPick = true;
    private void Awake()
    {
        _sanityManager = GameObject.FindWithTag("SanityManager").GetComponent<SanityManager>();
    }
    public void IsFound() 
    {
        if (firstPick)
        {
            _sanityManager.ChangeValue(1);
            firstPick = false;
        }
    }
    public void FixateKey(Transform t)
    {
        GetComponent<XRGrabInteractable>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        gameObject.SetActive(false);

    }
}
