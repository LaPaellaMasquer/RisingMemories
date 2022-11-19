using System.Collections;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float timeToLive;
    void Start()
    {
        StartCoroutine(Destruct());
    }

    public IEnumerator Destruct()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }
}
