<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebSpawnerp2 : MonoBehaviour
{
    [SerializeField] GameObject _web;
    public GameObject _actualWeb;
    bool _alreadyAWeb;

    private
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
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebSpawnerp2 : MonoBehaviour
{
    [SerializeField] GameObject _web;
    [SerializeField] private AudioClip spiderman = null;
    private AudioSource audiosource_spiderman;
    public GameObject _actualWeb;
    bool _alreadyAWeb;

    void Start()
    {
        audiosource_spiderman = GetComponent<AudioSource>();
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
                audiosource_spiderman.PlayOneShot(spiderman);
                // bruit de spiderman ici pls
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
            audiosource_spiderman.PlayOneShot(spiderman);
            _actualWeb = Instantiate(_web, transform.position, Quaternion.identity);
            _actualWeb.GetComponent<SpawnCobweb>()._cobwebList.Add(new Web());
        }

    }
}
>>>>>>> hugo
