using UnityEngine;

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
            else
            {
                Debug.Log(cameraTransform.forward);
                Teleport();
                Debug.Log(cameraTransform.forward);
            }
        }
    }
    public void Teleport()
    {
        Vector3 offset = cameraTransform.localPosition;
        player.position = middle.position - offset;
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0f;
        player.rotation = Quaternion.LookRotation(-cameraForward);
        if (charController != null)
        {
            charController.enabled = false;
            charController.enabled = true;
        }
    }
}
