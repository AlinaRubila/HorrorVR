using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTeleportation : MonoBehaviour
{
    Transform _player;
    SanityManager _sanityManager;
    SoundManager _soundManager;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip[] clips;
    [SerializeField] GameObject ghost;
    float[] teleportDistances = new float[] { 6f, 10f, 15f};
    float[] disappearTime = new float[] { 5f, 7f, 10f, 12f, 15f };
    float hideDistance = 5f;
    bool isHidden = true;
    bool gotPlace = false;
    Coroutine teleportCoroutine;
    System.Random rand = new System.Random();
    float targetDisappearTime;
    float disappearTimer = 0f;
    WaitForSeconds twoSeconds = new WaitForSeconds(2);
    private void Awake()
    {
        _player = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        _sanityManager = GameObject.FindWithTag("SanityManager").GetComponent<SanityManager>();
        _soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
        ghost.SetActive(false);
    }
    private void Start()
    {
        teleportCoroutine = StartCoroutine(Teleport());
    }
    private void OnDisable()
    {
        if (teleportCoroutine != null) StopCoroutine(teleportCoroutine);
    }
    IEnumerator Teleport()
    {
        while (true)
        {
            if (isHidden)
            {
                float waitTime = 2 * (_sanityManager.GetSanity() / 10f);
                yield return new WaitForSeconds(waitTime);
                TeleportGhost();
                continue;
            }
            else if (!isHidden && gotPlace)
            {
                float distance = Vector3.Distance(transform.position, _player.position);
                if (distance <= hideDistance) 
                {
                    Disappear();
                    disappearTimer = 0f;
                    yield return twoSeconds;
                    continue;
                }
                disappearTimer += Time.deltaTime;
                if (disappearTimer >= targetDisappearTime)
                {
                    Disappear();
                    yield return twoSeconds;
                }
            }
            yield return null;
        }
    }
    void TeleportGhost()
    {
        ghost.SetActive(true);
        isHidden = false;
        gotPlace = true;
        disappearTimer = 0f;
        targetDisappearTime = disappearTime[rand.Next(disappearTime.Length)];
        _soundManager.PlaySound(source, clips[rand.Next(5)]);
        Vector2 randomPos = Random.insideUnitCircle.normalized * (teleportDistances[rand.Next(3)]);
        transform.position = new Vector3(randomPos.x+_player.position.x, transform.position.y, randomPos.y+_player.position.z);
        Vector3 directionToPlayer = _player.position - transform.position;
        float angleY = Mathf.Atan2 (directionToPlayer.x, directionToPlayer.z) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, angleY+80f, transform.eulerAngles.z);
    }
    void Disappear()
    {
        gotPlace = false;
        ghost.SetActive(false);
        transform.position = new Vector3(0, transform.position.y, 0);
        isHidden = true;
    }
}
