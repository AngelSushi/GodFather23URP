using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebSpawnerp2 : MonoBehaviour
{
    [SerializeField] GameObject _web;
    [SerializeField] private AudioClip spiderman = null;
    private AudioSource audiosource_spiderman;
    public GameObject _actualWeb;
    bool _alreadyAWeb;

    public Slider slider;
    void Start()
    {
        audiosource_spiderman = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (_actualWeb != null)
        {
            Debug.Log(_actualWeb.GetComponent<SpawnCobweb>()._cobwebList.Count);
            Debug.Log(_actualWeb.GetComponent<SpawnCobweb>()._maxTotalOfWeb);
            Debug.Log("size " + (_actualWeb.GetComponent<SpawnCobweb>()._cobwebList.Count / _actualWeb.GetComponent<SpawnCobweb>()._maxTotalOfWeb));
            
            slider.value = (float) (_actualWeb.GetComponent<SpawnCobweb>()._cobwebList.Count / _actualWeb.GetComponent<SpawnCobweb>()._maxTotalOfWeb);
            
        }
        
        
        if (Input.GetMouseButtonDown(2))
        {
            if (!_alreadyAWeb)
            {
                _alreadyAWeb = !_alreadyAWeb;
                NewWeb();
            }
            else if(_actualWeb != null)
            {
    
                _actualWeb.GetComponent<SpawnCobweb>()._cobwebList.Add(new Web());
                _actualWeb.GetComponent<SpawnCobweb>().NewTriangle();
                audiosource_spiderman.PlayOneShot(spiderman);
                // bruit de spiderman ici pls
            }

            //dash
            StartCoroutine(GetComponent<CharacterMovementp2>().Dash());
        }
        if (Input.GetMouseButtonDown(0))
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
