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

    [SerializeField] private float bossAmplifier;
    [SerializeField] private float maxBossSize;
    private float _timer;

    private int _bossCount;

    private void Start()
    {
        StartCoroutine(BossTimer());
    }

    void Update()
    {
        _timer += Time.deltaTime;
        
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
        Vector3 scale = ennemyToSpawn.transform.localScale;

        if (_nextIsBoss)
        {
            ennemyToSpawn = boss;
            _bossCount++;
            _nextIsBoss = false;
            float scaleX = scale.x + bossAmplifier * _bossCount;
            float scaleY = scale.y + bossAmplifier * _bossCount;

            Mathf.Clamp(scaleX, 0, maxBossSize);
            Mathf.Clamp(scaleY, 0, maxBossSize);

            scale = new Vector3(scaleX, scaleY, scale.z);
        }

        respawn = Instantiate(ennemyToSpawn, spawnPoint.transform.position, spawnPoint.transform.rotation);
        GameManager._instance._listMonster.Add(respawn);
        respawn.transform.localScale = scale;
        
        
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
