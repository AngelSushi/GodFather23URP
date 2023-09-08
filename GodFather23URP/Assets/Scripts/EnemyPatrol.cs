using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyPatrol : MonoBehaviour
{

    [SerializeField] private float speed;
    
    private Vector2 destination;

    public Vector2 Destination
    {
        get => destination;
    }

    private Rigidbody2D rb;
    private Camera _mainCamera;

    [SerializeField] private int secureOffset;

    public int SecureOffset
    {
        get => secureOffset;
    }
    
    private int originSecureOffset;

    [SerializeField] private bool debugMode;

    public bool canBeStun;
    public bool isStunned;
    public float stunTimer;

    public float respawnStunTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;

        originSecureOffset = secureOffset;
        GenerateDestination();

    }

    private void Update()
    {
        if (canBeStun)
        {
            GetComponent<Animator>().SetBool("IsStun",isStunned);
        }
        
        rb.velocity = canBeStun ? isStunned ? Vector2.zero : (destination - (Vector2)transform.position).normalized * speed : (destination - (Vector2)transform.position).normalized * speed ;

        if (rb.velocity != Vector2.zero && GetComponent<Animator>().GetBool("IsStun"))
        {
            GetComponent<Animator>().SetBool("IsStun",false);
            isStunned = false;
            canBeStun = false;
            StartCoroutine(RespawnStun());
        }

        if (destination != Vector2.zero && !GameManager._instance.IsInBoss)
        {
            Vector2 normalizedDestination = destination.normalized;
            float angle = Mathf.Atan2(-normalizedDestination.x, normalizedDestination.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0,0,angle);
        }
        
        if (Vector2.Distance(destination, transform.position) < 0.25f)
        {
            GenerateDestination();
        }

        if (debugMode)
        {
            Debug.DrawLine(transform.position,destination,Color.magenta);
        }

    }

    private void GenerateDestination()
    {
        Vector2 startCameraPos = (Vector2) _mainCamera.transform.position - GetCameraBounds();
        Vector2 endCameraPos = (Vector2)_mainCamera.transform.position + GetCameraBounds();
        
        GenerateDestination(startCameraPos.x,endCameraPos.x,startCameraPos.y,endCameraPos.y);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Cobweb") && canBeStun && !isStunned)
        {
            isStunned = true;
            StartCoroutine(WaitStun());
        }
    }


    private IEnumerator WaitStun()
    {
        yield return new WaitForSeconds(stunTimer);

        isStunned = false;
        canBeStun = false;
        StartCoroutine(RespawnStun());
    }

    private IEnumerator RespawnStun()
    {
        yield return new WaitForSeconds(respawnStunTimer);

        canBeStun = true;
    }

    private void GenerateDestination(float xMin,float xMax,float yMin,float yMax)
    {
        float randomX = Random.Range(xMin,xMax);
        float randomY = Random.Range(yMin, yMax);

        destination = new Vector2(randomX, randomY);
        
        
        //Debug.Log("distance " + Vector2.Distance(destination, transform.position));
        
        secureOffset = Vector2.Distance(destination, transform.position) < originSecureOffset ? (int)Vector2.Distance(destination, transform.position) : originSecureOffset;
    }

    private Vector2 GetCameraBounds() => new Vector2(_mainCamera.orthographicSize * Screen.width / Screen.height, _mainCamera.orthographicSize);
}
