using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceOnWeb : MonoBehaviour
{
    [SerializeField] Transform _spider;
    Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        AddForce();
        
    }

    void AddForce()
    {
        float deplacementHorizontal = Input.GetAxis("Horizontal");
        float deplacementVertical = Input.GetAxis("Vertical");

        Vector2 deplacement = new Vector2(deplacementHorizontal, deplacementVertical) * 3;

        _rb.velocity = deplacement;
    }
}
