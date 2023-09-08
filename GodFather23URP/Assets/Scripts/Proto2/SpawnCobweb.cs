using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCobweb : MonoBehaviour
{
    [SerializeField] GameObject _cobweb;
    [SerializeField] GameObject _checkpoint;
    [SerializeField] int _maxAmountOfWeb;
    public int _maxTotalOfWeb;
    public int _maxAmountOfCheckpoint;
    [SerializeField] float _spawnRange;
    //int _amountOfWeb;
    [SerializeField] GameObject _spider;
    GameObject _lastWeb;
    bool _isTheLast = true;
    public List<Web> _cobwebList = new List<Web>();
    int _cobwebID = 0;
    public List<GameObject> _triangles = new List<GameObject>();

    [HideInInspector] public int _webAmount;
    [HideInInspector] public bool _isDead = true;
    void Start()
    {
        _spider = GameObject.FindGameObjectWithTag("Player");
        //StartCoroutine(Coro());
        _lastWeb = gameObject;
        _triangles.Add(gameObject);
    }

    void Update()
    {
        //Debug.Log((_spider.transform.position - transform.position).magnitude);
        //Debug.Log("cobweb id : " + _cobwebID + " cobweb count : " + _cobwebList[_cobwebID].Cobwebs.Count);-
        if ((_spider.transform.position - _lastWeb.transform.position).magnitude > _spawnRange && _cobwebList[_cobwebID].Cobwebs.Count < _maxAmountOfWeb && _triangles.Count < _maxAmountOfCheckpoint && _isDead)
        //if ((_spider.transform.position - _lastWeb.transform.position).magnitude > _spawnRange && _cobwebList[_cobwebID].Cobwebs.Count < _maxAmountOfWeb && _maxTotalOfWeb >_webAmount)
        {
            NewWeb();
            //Debug.Log(_lastWeb.name);
        }
        else if ((_cobwebList[_cobwebID].Cobwebs.Count >= _maxAmountOfWeb && _isTheLast ))
        {
            _isTheLast = !_isTheLast;
            _lastWeb.GetComponent<HingeJoint2D>().enabled = true;

            _lastWeb.GetComponent<HingeJoint2D>().connectedBody = _spider.GetComponent<Rigidbody2D>();
            //_lastWeb.transform.parent = _spider.transform;
            //AddForce();
        }
        else
        {
            //Debug.Log("here");
        }

        if(_cobwebList[_cobwebList.Count - 1].Cobwebs.Count > 2 )
            ShouldWeCutTheWire();
        
    }
    public void NewTriangle()
    {
        _cobwebID++;
        _isTheLast = true;
        _lastWeb.GetComponent<HingeJoint2D>().enabled = true;
        GameObject _newWeb = Instantiate(_checkpoint, _lastWeb.GetComponent<CobwebScript>()._pivot.position, _lastWeb.GetComponent<Transform>().localRotation);


        _lastWeb.GetComponent<HingeJoint2D>().connectedBody = _newWeb.GetComponent<Rigidbody2D>();
        _lastWeb = _newWeb;

        _lastWeb.GetComponent<CobwebScript>()._id = _webAmount;
        _lastWeb.GetComponent<CobwebScript>()._wire = _cobwebID;
        _webAmount++;

        _triangles.Add(_newWeb);
    }


    void NewWeb()
    {
        if(_lastWeb != gameObject)
        {
            _lastWeb.GetComponent<HingeJoint2D>().enabled = true;
            GameObject _newWeb = Instantiate(_cobweb, _lastWeb.GetComponent<CobwebScript>()._pivot.position,_lastWeb.GetComponent<Transform>().localRotation);


            _lastWeb.GetComponent<HingeJoint2D>().connectedBody = _newWeb.GetComponent<Rigidbody2D>();
            _lastWeb = _newWeb;
        }
        else
        {
            GameObject _newWeb = Instantiate(_cobweb, transform.position, Quaternion.identity);
            GetComponent<HingeJoint2D>().connectedBody = _newWeb.GetComponent<Rigidbody2D>();
            _lastWeb = _newWeb;
        }
        //on rajoute un ID au cobew
        _lastWeb.GetComponent<CobwebScript>()._id = _webAmount;
        _lastWeb.GetComponent<CobwebScript>()._wire = _cobwebID;
        _webAmount++;


        Rotation(_lastWeb.transform);
        _cobwebList[_cobwebID].Cobwebs.Add(_lastWeb);
        //_cobweb.transform.LookAt(_spider.transform);
    }
    public void Rotation(Transform _transform)
    {
        Vector3 _direction = _transform.position - _spider.transform.position;
        _direction.z = 0; // Assurez-vous que la direction reste en 2D

        if (_direction != Vector3.zero)
        {
            // Calculez l'angle d'orientation en radians et convertissez-le en degr�s
            float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

            // Appliquez la rotation pour orienter l'objet vers la cible
            _transform.rotation = Quaternion.Euler(new Vector3(0, 0,_angle - 90));
        }
    }

    public void SelfDestruct()
    {
        for(int _wire = 0; _wire < _cobwebList.Count; _wire++)
        {
            for(int _web = 0; _web < _cobwebList[_wire].Cobwebs.Count; _web++)
            {
                _cobwebList[_wire].Cobwebs[_web].GetComponent<WebDestroy>().DestroyTheWeb();
            }
        }

        for(int _checkpoints = _triangles.Count; _checkpoints > 0; _checkpoints--)
        {
            Destroy(_triangles[_checkpoints - 1], .5f);
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<WebSpawnerp2>().NoWeb();

    }


    void ShouldWeCutTheWire()
    {
        //Debug.Log("yo");
        GameObject _lastWeb = _cobwebList[_cobwebList.Count - 1].Cobwebs[_cobwebList[_cobwebList.Count - 1].Cobwebs.Count - 1];
        GameObject _beforeLastWeb = _cobwebList[_cobwebList.Count - 1].Cobwebs[_cobwebList[_cobwebList.Count - 1].Cobwebs.Count - 2];
        RaycastHit2D[] hits = Physics2D.RaycastAll(_beforeLastWeb.transform.position, (_beforeLastWeb.transform.position - _lastWeb.transform.position).normalized, (_beforeLastWeb.transform.position-_lastWeb.transform.position).magnitude * 2);
        //Debug.Log("center : " + _lastWeb.transform.position + " direction : " + _lastWeb.transform.forward + " distance : " + (_lastWeb.transform.position - _cobwebList[_cobwebList.Count-1].Cobwebs[_cobwebList[_cobwebList.Count-1].Cobwebs.Count - 2].transform.position).magnitude);
        // Parcourez toutes les collisions d�tect�es.
        foreach (RaycastHit2D hit in hits)
        {
            // V�rifiez si le collider appartient � un autre objet (�vite de d�tecter lui-m�me).
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Cobweb")
                {
                    if(hit.collider.GetComponent<CobwebScript>()._wire != _lastWeb.GetComponent<CobwebScript>()._wire &&
                        hit.collider.GetComponent<CobwebScript>()._wire + 1 != _lastWeb.GetComponent<CobwebScript>()._wire)
                    {
                        //Debug.Log("wire : " + hit.collider.GetComponent<CobwebScript>()._wire);
                        //GameObject.FindGameObjectWithTag("Player").GetComponent<WebSpawnerp2>()._actualWeb.GetComponent<>
                        SelfDestruct();
                    }

                }


            }
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(_spider.transform.position, _spawnRange);
    }


}
