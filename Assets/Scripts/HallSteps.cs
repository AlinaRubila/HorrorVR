using System.Collections;
using UnityEngine;

public class HallSteps : MonoBehaviour
{
    [SerializeField] Transform[] _soundPoints;
    [SerializeField] Transform _sourcePos;
    [SerializeField] AudioSource _source;
    void Start()
    {
        StartCoroutine(PlaySound());
    }
    IEnumerator PlaySound()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 10));
            Transform teleportPoint = _soundPoints[Random.Range(0, _soundPoints.Length)];
            _source.transform.position = teleportPoint.position;
            _source.Play();
        }
    }
}
