using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class GhostTeleportation : MonoBehaviour
{
    Transform player;
    [SerializeField] private GameObject ghost;
    float[] teleportDistances = new float[] { 6f, 10f, 15f};
    float[] disappearTime = new float[] { 5f, 7f, 10f, 12f, 15f };
    float hideDistance = 5f;
    bool isHidden = false;
    int sanityLevel = 10;
    float timer = 0f;
    bool gotPlace = false;
    private void Awake()
    {
        player = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if ((timer >= 2 * (sanityLevel / 10) && !gotPlace) || (timer > 10 * (sanityLevel / 10))) TeleportGhost();
        System.Random rand = new System.Random();
        float randomTime = disappearTime[rand.Next(5)];
        if (gotPlace && timer >= randomTime && timer < randomTime + 1) Disappear();
        if (System.Math.Round(timer) % 2 == 0) CheckDistance(); 
    }
    void TeleportGhost()
    {

        if (isHidden) 
        {
            ghost.SetActive(true);
            isHidden = false;
        }
        gotPlace = true;
        System.Random rand = new System.Random();
        Vector3 randomPos = Random.onUnitSphere * teleportDistances[rand.Next(3)] + player.position;
        transform.position = new Vector3(randomPos.x, transform.position.y, randomPos.z);
        Vector3 directionToPlayer = player.position - transform.position;
        float angleY = Mathf.Atan2 (directionToPlayer.x, directionToPlayer.z) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, angleY+80f, transform.eulerAngles.z);
        timer = 0f;
    }
    void CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= hideDistance && gotPlace) Disappear();
     }
    void Disappear()
    {
        gotPlace = false;
        ghost.SetActive(false);
        transform.position = new Vector3(0, transform.position.y, 0);
        isHidden = true;
        timer = 0f;
    }
}
