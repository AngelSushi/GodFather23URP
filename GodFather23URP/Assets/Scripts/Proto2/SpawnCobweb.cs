using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCobweb : MonoBehaviour
{
    [SerializeField] GameObject _cobweb;
    [SerializeField] GameObject _checkpoint;
    [SerializeField] int _maxAmountOfWeb;
    [SerializeField] float _spawnRange;
    //int _amountOfWeb;
    [SerializeField] GameObject _spider;
    GameObject _lastWeb;
    bool _isTheLast = true;
    public List<Web> _cobwebList = new List<Web>();
    int _cobwebID = 0;
    public List<GameObject> _triangles = new List<GameObject>();

    void Start()
    {
        _spider = GameObject.FindGameObjectWithTag("Player");
        //StartCoroutine(Coro());
        _lastWeb = gameObject;
    }

    void Update()
    {
        //Debug.Log((_spider.transform.position - transform.position).magnitude);
        Debug.Log("cobweb id : " + _cobwebID + " cobweb count : " + _cobwebList[_cobwebID].Cobwebs.Count);
        if ((_spider.transform.position - _lastWeb.transform.position).magnitude > _spawnRange && _cobwebList[_cobwebID].Cobwebs.Count < _maxAmountOfWeb)
        {
            NewWeb();
            //Debug.Log(_lastWeb.name);
        }
        else if (_cobwebList[_cobwebID].Cobwebs.Count >= _maxAmountOfWeb && _isTheLast)
        {
            _isTheLast = !_isTheLast;
            _lastWeb.GetComponent<HingeJoint2D>().enabled = true;

            _lastWeb.GetComponent<HingeJoint2D>().connectedBody = _spider.GetComponent<Rigidbody2D>();
            //_lastWeb.transform.parent = _spider.transform;
            //AddForce();
        }
    }
    public void NewTriangle()
    {
        _cobwebID++;
        _isTheLast = true;
        _lastWeb.GetComponent<HingeJoint2D>().enabled = true;
        GameObject _newWeb = Instantiate(_checkpoint, _lastWeb.GetComponent<CobwebScript>()._pivot.position, _lastWeb.GetComponent<Transform>().localRotation);


        _lastWeb.GetComponent<HingeJoint2D>().connectedBody = _newWeb.GetComponent<Rigidbody2D>();
        _lastWeb = _newWeb;
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
            // Calculez l'angle d'orientation en radians et convertissez-le en degrés
            float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

            // Appliquez la rotation pour orienter l'objet vers la cible
            _transform.rotation = Quaternion.Euler(new Vector3(0, 0,_angle - 90));
        }
    }

    void AddForce()
    {
        Vector2 direction = (_lastWeb.transform.position - _spider.transform.position).normalized;

        // Appliquez la vélocité pour se déplacer vers la cible
        _lastWeb.GetComponent<Rigidbody2D>().velocity = direction * -1 * _spider.GetComponent<CharacterMovementp2>()._spiderSpeed;

    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(_spider.transform.position, _spawnRange);
    }


}
