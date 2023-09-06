using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class timerDead : MonoBehaviour
{
    public float timer;


    void Start()
    {
        StartCoroutine(endTimer());
    }

    private IEnumerator endTimer()
    {
        yield return new WaitForSeconds(timer);
        FindObjectOfType<spawn>().spawnList.Remove(transform.position);
        Destroy(gameObject);
    }

    public void restartTimer ()
    {
        StopCoroutine(endTimer());
        StartCoroutine(endTimer());
    }

}
