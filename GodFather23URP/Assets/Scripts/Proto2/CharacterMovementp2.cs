using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementp2 : MonoBehaviour
{
    [SerializeField] float _spiderSpeed;
    Rigidbody2D _rb2d;
    Vector2 _position;

    Vector2 velocity = Vector2.zero;


    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _position = new Vector2(Camera.main.transform.position.x - Camera.main.pixelWidth/2, Camera.main.transform.position.y - Camera.main.pixelHeight/2);
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        //renvoie la position de la souris en fonction du monde
        Vector2 _mousePosition = new Vector2(Input.mousePosition.x + Camera.main.transform.position.x - Camera.main.pixelWidth / 2, Input.mousePosition.y + Camera.main.transform.position.y - Camera.main.pixelHeight / 2);
        //on scale avec la size de la camera
        _mousePosition *= Camera.main.orthographicSize * 2 / Camera.main.pixelHeight;
        transform.position = Vector2.SmoothDamp(transform.position, _mousePosition, ref velocity, 1/_spiderSpeed);
    }
}
