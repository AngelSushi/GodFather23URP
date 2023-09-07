using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyPatrol : MonoBehaviour
{

    [SerializeField] private float speed;
    
    [SerializeField] private Vector2 destination;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GenerateDestination();
    }

    private void Update()
    {
        Debug.Log("distance " + Vector2.Distance(transform.position, (Vector2)transform.position + destination));
        rb.velocity = destination.normalized * speed;
    }


    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.75f);
        
        destination = /*((Vector2)transform.position +*/ Random.insideUnitCircle * 10;
        StartCoroutine(Wait());
    }
    private void GenerateDestination()
    {
        destination = /*((Vector2)transform.position +*/ Random.insideUnitCircle * 10;
        StartCoroutine(Wait());
    }
}
