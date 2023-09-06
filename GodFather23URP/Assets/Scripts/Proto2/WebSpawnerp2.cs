using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebSpawnerp2 : MonoBehaviour
{
    [SerializeField] float _delay = .5f;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] GameObject _web;
    void Start()
    {
        StartCoroutine(SpawnWeb());
    }

    void Update()
    {
    }

    IEnumerator SpawnWeb()
    {
        yield return new WaitForSeconds(_delay);
        GameObject _newWeb = Instantiate(_web, _spawnPoint.position,Quaternion.identity);
        //_newWeb.transform.LookAt(transform);
        _newWeb.GetComponent<HingeJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
    }

}
