using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobwebScript : MonoBehaviour
{
    public Transform _pivot;
    public int _id;
    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if(_collision.collider.tag == "Cobweb")
        {
            Debug.Log(_collision.collider.GetComponent<CobwebScript>()._id);
                
        }
        //Debug.Log(_collision.collider.name);
    }
}
