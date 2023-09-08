using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class spawn : MonoBehaviour
{

    public float size;

    [SerializeField] private List<GameObject> insectsPrefab;
    [SerializeField] public GameObject boss;
    [SerializeField] private float bossTimer;
  //  public bool _nextIsBoss;
    
    public GameObject spawnPoint;
    public GameObject respawn;

    public float margin;

    public List<Vector2>  spawnList = new List<Vector2>();

    public float spawnCooldown = 3f;
    public bool Cooldown = true;

    [SerializeField] Vector2 _pointA;
    [SerializeField] Vector2 _pointB;
    [SerializeField] float _sideSize = 2;

    private void Start()
    {
      //  StartCoroutine(BossTimer());
    }

    void Update()
    {
        
        if (Cooldown && !GameManager._instance.IsInBoss)
        {
            random();
        }
    }

    public void random ()
    {

        Vector3 _positon = new Vector3(Random.Range(_pointA.x - _sideSize, _pointB.x + _sideSize), Random.Range(_pointA.y - _sideSize, _pointB.y + _sideSize), 8);
        //Debug.Log(_positon); 
        //detection 
        if (_positon.x > _pointB.x || _positon.x < _pointA.x || _positon.y > _pointB.y || _positon.y < _pointA.y)
            random();
        else
        {
            //SpawnEnnemy(_positon);
            Cooldown = false;
            StartCoroutine(spawnTime());

            int random = Random.Range(0, insectsPrefab.Count);

            GameObject ennemyToSpawn = insectsPrefab[random];
            GameManager._instance.SpawnEnemy(ennemyToSpawn, _positon);
        }
        /*
        Vector3 position = Random.insideUnitCircle * size;
        position.z = 8f;
       
        spawnPoint.transform.position =position;
        Cooldown = false;
        StartCoroutine(spawnTime());

        bool succeed = true;

        if(spawnList.Count == 0)
        {
            SpawnEnnemy();
            return;
        }

        foreach (Vector2 trez in spawnList)
        {
            if (Vector2.Distance(trez, spawnPoint.transform.position) < margin)
            {
                succeed = false;
                break;
            }
        }


        if (succeed)
        {
            SpawnEnnemy();
        }
        else random();
        */
    }

    private void SpawnEnnemy()
    {
        spawnList.Add(spawnPoint.transform.position);

        int random = Random.Range(0, insectsPrefab.Count);

        GameObject ennemyToSpawn = insectsPrefab[random];

     /*   if (_nextIsBoss)
        {
            ennemyToSpawn = boss;
            _nextIsBoss = false;
        }
        */
        //respawn = Instantiate(ennemyToSpawn, spawnPoint.transform.position, spawnPoint.transform.rotation);
       // GameManager._instance._listMonster.Add(respawn);

    }

    private IEnumerator spawnTime()
    {
        yield return new WaitForSeconds(spawnCooldown);
        Cooldown = true;
    }

  /*  private IEnumerator BossTimer()
    {
        yield return new WaitForSeconds(bossTimer);

        _nextIsBoss = true;
        StartCoroutine(BossTimer());
    }
*/
    void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(spawnPoint.transform.position, size); 
        Gizmos.DrawLine(new Vector3(_pointA.x, _pointA.y, 0), new Vector3(_pointA.x, _pointB.y, 0));
        Gizmos.DrawLine(new Vector3(_pointB.x, _pointB.y, 0), new Vector3(_pointB.x, _pointA.y, 0));
        Gizmos.DrawLine(new Vector3(_pointA.x, _pointB.y, 0), new Vector3(_pointA.x, _pointA.y, 0));
        Gizmos.DrawLine(new Vector3(_pointB.x, _pointA.y, 0), new Vector3(_pointB.x, _pointB.y, 0));
    }
}
