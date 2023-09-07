using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if(_collision.tag == "Cobweb")
        {
            //Debug.Log("wire : " + _collision.GetComponent<CobwebScript>()._wire + " checkpoint : " + GetComponent<CobwebScript>()._wire);
            if (GetComponent<CobwebScript>()._wire != _collision.GetComponent<CobwebScript>()._wire &&
                GetComponent<CobwebScript>()._wire != _collision.GetComponent<CobwebScript>()._wire + 1 &&
                GetComponent<CobwebScript>()._wire != _collision.GetComponent<CobwebScript>()._wire - 1)
            {
                GameManager._instance.WebIsFinish();
            }
        }
        //Debug.Log(_collision.GetComponent<CobwebScript>()._wire);

        
    }
}
