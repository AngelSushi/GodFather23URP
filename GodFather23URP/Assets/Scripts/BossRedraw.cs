using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRedraw : MonoBehaviour
{
    public GameObject boss;


    private void Start()
    {
        Debug.Log("player " + FindObjectOfType<Player>().collideBoss);
        boss = FindObjectOfType<Player>().collideBoss;
    }

    // Update is called once per frame
    void Update()
    {
        if (boss == null)
        {
            boss = FindObjectOfType<Player>().collideBoss;
        }
        if (boss != null)
        {
            Debug.Log("boss");
            transform.GetComponent<SpriteRenderer>().sprite = boss.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
