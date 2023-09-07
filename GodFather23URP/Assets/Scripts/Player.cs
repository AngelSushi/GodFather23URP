using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{

    private List<FormTest> _allForms;
    private FormTest _current;

    [SerializeField] private float speed;

    [SerializeField] private GameObject gfx;

    private int _previewIndex;

    private void Start()
    {
        _allForms = FindObjectsOfType<FormTest>().ToList();
        _allForms.ForEach(form => form.gameObject.SetActive(false));
    }

    private void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.Translate(direction * speed * Time.deltaTime);
        
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
            gfx.transform.rotation = Quaternion.Euler(0,0,angle);
        }
        
    }


    private void OnCollisionStay2D(Collision2D col)
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            int random = Random.Range(0, _allForms.Count - 1);
            _current = _allForms[random];
        
            _current.transform.gameObject.SetActive(true);

            _previewIndex = 0;
            _current.transform.GetChild(_previewIndex).GetComponent<Animator>().enabled = true;

            StartCoroutine(WaitPreviewEnd());

        }
    }

    private IEnumerator WaitPreviewEnd()
    {
        yield return new WaitForSeconds(1f);

        if (_current != null)
        {
            _current.transform.GetChild(_previewIndex).GetComponent<Animator>().enabled = false;
            _previewIndex++;

            if (_previewIndex >= _current.transform.childCount)
            {
                _previewIndex = 0;
            }
            
            _current.transform.GetChild(_previewIndex).GetComponent<Animator>().enabled = true;

            StartCoroutine(WaitPreviewEnd());
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        _current?.transform.gameObject.SetActive(false);
        _current = null;
    }
}
