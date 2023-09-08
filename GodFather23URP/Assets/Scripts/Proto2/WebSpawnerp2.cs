using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebSpawnerp2 : MonoBehaviour
{
    [SerializeField] GameObject _web;
    public GameObject _actualWeb;
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

            //dash
            StartCoroutine(GetComponent<CharacterMovementp2>().Dash());
        }
        if (Input.GetKeyDown("escape"))
        {
            _actualWeb.GetComponent<SpawnCobweb>().SelfDestruct();
            _actualWeb.GetComponent<SpawnCobweb>()._isDead = false;
        }
    }
    public void NoWeb()
    {
        _alreadyAWeb = false;
        //Debug.Log(_alreadyAWeb);
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
