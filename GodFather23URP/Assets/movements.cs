using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movements : MonoBehaviour
{
    public float speed = 5f;

    public Rigidbody2D rb;
    private Vector2 deplace;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        deplace.x = Input.GetAxisRaw("Horizontal");
        deplace.y = Input.GetAxisRaw("Vertical");
    }
}
