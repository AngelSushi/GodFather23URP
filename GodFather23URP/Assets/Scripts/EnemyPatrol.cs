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
        
        
        // Si collision ou calcul
        
        
       /* foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, 0.5f,  1 << 6))
        {
            if (collider != null && collider.gameObject != gameObject)
            {
                Debug.Log("is on collision with " + collider.gameObject.name);
                EditorApplication.isPaused = true;

                     
                
                
            }
        }
        */

      /* foreach (GameObject enemy in GameManager._instance._listMonster)
       {
           if (enemy != gameObject)
           {
               Debug.Log("value " + GameManager._instance.patrolCheckers.FirstOrDefault(checker => checker.firstTarget == gameObject && checker.secondTarget == enemy));
               
               if (GameManager._instance.patrolCheckers.FirstOrDefault(checker => checker.firstTarget == gameObject && checker.secondTarget == enemy) == null)
               {
                   
                   GameManager._instance.patrolCheckers.Add(new GameManager.PatrolChecker(gameObject,enemy));
                   // Vector2 checkVectorOrigin = (Vector2)transform.position + (destination - (Vector2)transform.position).normalized * secureOffset;
              
                  // Utiliser des Bounds pour intersect
                   Vector2 interpolateOriginPosition = (Vector2)transform.position + rb.velocity * 1; // The position of the first enemy in 1 seconde 
                   Vector2 interpolateCurrentPosition = (Vector2)enemy.transform.position + enemy.GetComponent<Rigidbody2D>().velocity * 1;

                   if (debugMode)
                   {
                       Debug.DrawLine(transform.position,interpolateOriginPosition,Color.yellow);
                       Debug.DrawLine(transform.position,(Vector2)transform.position + Vector2.up * 5,Color.red);
                   }
                   
                   //Vector2 checkVectorCurrent = (Vector2)enemy.transform.position + (enemy.GetComponent<EnemyPatrol>().Destination - (Vector2)enemy.transform.position).normalized * enemy.GetComponent<EnemyPatrol>().SecureOffset;
               
                   if (CheckIntersection(transform.position, interpolateOriginPosition, enemy.transform.position,interpolateCurrentPosition))
                   {
                       Debug.Log("super il y a intersection omg" );
                       
                       // il faut générer une direction vers laquelle il ne touchera pas l'enemy 

                       /*float angle = Vector2.Angle(Vector2.up, destination);

                       Vector2 interpolateOriginDir = (Vector2)transform.position + rb.velocity * 1 * (interpolateOriginPosition * Mathf.Cos(angle * Mathf.Deg2Rad));
                       Vector2 interpolateCurrentDir = interpolateCurrentPosition + enemy.GetComponent<Rigidbody2D>().velocity * 1; // IN 2 seconds
                       
                       while (CheckIntersection(transform.position,interpolateOriginDir,interpolateCurrentPosition,interpolateCurrentDir))
                       {
                           Debug.Log("enter");
                           angle += 5;
                           interpolateOriginDir = (Vector2)transform.position + rb.velocity * (interpolateOriginPosition * Mathf.Cos(angle * Mathf.Deg2Rad));
                           
                       }
                       
                       Debug.DrawLine(transform.position,interpolateOriginDir,Color.cyan);
                       Debug.Break();
                       

                       destination *= -1;

                       GameManager._instance.patrolCheckers.Remove(GameManager._instance.patrolCheckers.First(checker => checker.firstTarget == gameObject && checker.secondTarget == enemy));
                       // Detecter dans une certaine direction 
                   }    
               }
               
               
              
           }
           
       } 

        /*  if (Physics2D.RaycastAll(transform.position, destination.normalized, 0.5f, 1 << 6).FirstOrDefault(hit => hit.collider != null && hit.collider.gameObject != gameObject) != null)
          {
              Debug.Log("collision with");
              EditorApplication.isPaused = true;
              
          }
          
          */
        
    }

    private bool CheckIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    {
        float denominator = ((p2.x - p1.x) * (p4.y - p3.y)) - ((p2.y - p1.y) * (p4.x - p3.x));
        float numerator1 = ((p1.y - p3.y) * (p4.x - p3.x)) - ((p1.x - p3.x) * (p4.y - p3.y));
        float numerator2 = ((p1.y - p3.y) * (p2.x - p1.x)) - ((p1.x - p3.x) * (p2.y - p1.y));

        if (denominator != 0)
        {
            float t1 = numerator1 / denominator;
            float t2 = numerator2 / denominator;

            return (t1 >= 0 && t1 <= 1) && (t2 >= 0 && t2 <= 1);
        }

        return false;
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
            Debug.Log("stunnn mee");
            isStunned = true;
            StartCoroutine(WaitStun());
        }
    }


    private IEnumerator WaitStun()
    {
        yield return new WaitForSeconds(stunTimer);

        Debug.Log("unstunned me");
        isStunned = false;

        canBeStun = false;
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
