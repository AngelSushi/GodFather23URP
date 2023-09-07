using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebSpawnerp2 : MonoBehaviour
{
    [SerializeField] GameObject _web;
    GameObject _actualWeb;
    bool _alreadyAWeb;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (!_alreadyAWeb)
            {
                _alreadyAWeb = !_alreadyAWeb;
                NewWeb();
            }
            else
            {

                _actualWeb.GetComponent<SpawnCobweb>()._cobwebList.Add(new Web());
                _actualWeb.GetComponent<SpawnCobweb>().NewTriangle();
            }
        }
    }
    
    void NewWeb()
    {
        if (_alreadyAWeb)
        {
            _actualWeb = Instantiate(_web, transform.position, Quaternion.identity);
            _actualWeb.GetComponent<SpawnCobweb>()._cobwebList.Add(new Web());
        }

    }
}
