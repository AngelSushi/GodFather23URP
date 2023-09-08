using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAnim : MonoBehaviour
{
    public float effectDuration;
    private float timer;

    private void Start()
    {
        timer = effectDuration;
        StartCoroutine(DestroyAnim());
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        
        transform.Translate(Vector2.up * Time.deltaTime);

        transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(transform.gameObject.GetComponent<SpriteRenderer>().color.r,
            transform.gameObject.GetComponent<SpriteRenderer>().color.g,
            transform.gameObject.GetComponent<SpriteRenderer>().color.b,
            timer / effectDuration);
    }

    private IEnumerator DestroyAnim()
    {
        yield return new WaitForSeconds(effectDuration + 0.2f);
        Destroy(gameObject);
    }
}
