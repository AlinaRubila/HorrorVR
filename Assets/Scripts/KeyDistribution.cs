using UnityEngine;

public class KeyDistribution : MonoBehaviour
{
    [SerializeField] GameObject[] keys;

    void Start()
    {
        if (keys != null) 
        { 
            int randomNumber = Random.Range(0, keys.Length);
            keys[randomNumber].tag = "RightKey";
        }
    }
}
