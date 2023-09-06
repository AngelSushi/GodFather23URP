using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{

    private List<FormTest> _allForms;
    private FormTest _current;

    [SerializeField] private float speed;

    private void Start()
    {
        _allForms = FindObjectsOfType<FormTest>().ToList();
        
        _allForms.ForEach(form => form.gameObject.SetActive(false));
    }

    private void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0);
    }


    private void OnCollisionStay2D(Collision2D col)
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            int random = Random.Range(0, _allForms.Count);
            _current = _allForms[random];
        
            _current.transform.gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        _current?.transform.gameObject.SetActive(false);
    }
}
