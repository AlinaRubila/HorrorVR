using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Key : MonoBehaviour
{
    SanityManager _sanityManager;
    bool firstPick = true;
    Vector3 _startPosition;
    XRGrabInteractable _grab;
    private void Awake()
    {
        _sanityManager = GameObject.FindWithTag("SanityManager").GetComponent<SanityManager>();
        _startPosition = transform.position;
        _grab = GetComponent<XRGrabInteractable>();
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
        _grab.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        gameObject.SetActive(false);
    }
    public void BackToStart()
    {
        if (firstPick) return;
        _grab.enabled = false;
        transform.position = _startPosition;
        _grab.enabled = true;
    }
}
