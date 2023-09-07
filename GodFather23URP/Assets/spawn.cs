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
    [SerializeField] private GameObject boss;
    [SerializeField] private float bossTimer;
    private bool _nextIsBoss;
    
    public GameObject spawnPoint;
    public GameObject respawn;

    public float margin;

    public List<Vector2>  spawnList = new List<Vector2>();

    public float spawnCooldown = 3f;
    public bool Cooldown = true;


    private void Start()
    {
        StartCoroutine(BossTimer());
    }

    void Update()
    {
        
        if (Cooldown)
        {
            random();
        }
    }

    public void random ()
    {
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
    }

    private void SpawnEnnemy()
    {
        spawnList.Add(spawnPoint.transform.position);

        int random = Random.Range(0, insectsPrefab.Count);

        GameObject ennemyToSpawn = insectsPrefab[random];

        if (_nextIsBoss)
        {
            ennemyToSpawn = boss;
            _nextIsBoss = false;
        }
        
        respawn = Instantiate(ennemyToSpawn, spawnPoint.transform.position, spawnPoint.transform.rotation);
        GameManager._instance._listMonster.Add(respawn);

        foreach (Vector2 truc in spawnList)
        {
            print(truc);
            print(spawnList.Count);
        }
        
    }

    private IEnumerator spawnTime()
    {
        yield return new WaitForSeconds(spawnCooldown);
        Cooldown = true;
    }

    private IEnumerator BossTimer()
    {
        yield return new WaitForSeconds(bossTimer);

        _nextIsBoss = true;
        StartCoroutine(BossTimer());
    }
}
