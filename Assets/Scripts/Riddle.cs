using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;

public class Riddle : MonoBehaviour
{
    [SerializeField] bool rightEnd;
    [SerializeField] Transform middle;
    [SerializeField] GameObject portal;
    [SerializeField] Rigidbody door;
    Transform player;
    Transform cameraTransform;
    CharacterController charController;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        cameraTransform = Camera.main.transform;
        charController = player.GetComponent<CharacterController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactor"))
        {
            if (rightEnd)
            {
                door.isKinematic = false;
                portal.SetActive(true);
            }
            else Teleport();
        }
    }
    public void Teleport()
    {
        if (charController == null) return;
        charController.enabled = false;
        Vector3 offset = cameraTransform.position - player.position;
        player.position = middle.position - offset;
        charController.enabled = true;
    }
}
