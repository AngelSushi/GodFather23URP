using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FormTest : MonoBehaviour
{

    [SerializeField] private bool canDraw;

    [SerializeField] private List<GameObject> models = new List<GameObject>();
    
    private List<GameObject> _points = new List<GameObject>();

    [SerializeField] private TrailRenderer trailRenderer;

    private bool _succeed;
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            models.Add(transform.GetChild(i).gameObject);
        }

        trailRenderer.enabled = false;
    }
    private void OnMouseEnter()
    {
        canDraw = true;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 4;
        trailRenderer.enabled = true;
        trailRenderer.transform.position = worldPosition;
    }

    private void OnMouseExit()
    {
        _points.Clear();
        trailRenderer.Clear();
        trailRenderer.enabled = false;
        canDraw = false;
    }

    private void OnMouseDrag()
    {
        if (canDraw)
        {
            trailRenderer.gameObject.SetActive(true);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 4;
            trailRenderer.transform.position = worldPosition;
            trailRenderer.enabled = true;
            
            foreach (Collider2D collider in Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.14f)) {
                if (collider is CircleCollider2D && !_points.Contains(collider.gameObject))
                {
                    _points.Add(collider.gameObject);
                }
            }
        }
        
        
       
    }


    private void OnMouseUp()
    {
        if (canDraw)
        {
            if (_points.Count != models.Count)
            {
                Debug.Log("loose (" + _points.Count + "/" + models.Count + ")");
            }
            else
            {

                bool goodPath = true;
                GameObject pointDifferent = null;
                GameObject modelDifferent = null;
                
                for (int i = 0; i < _points.Count; i++)
                {
                    if (_points[i] != models[i])
                    {
                        goodPath = false;
                        pointDifferent = _points[i];
                        modelDifferent = models[i];
                        break;
                    }
                }

                if (goodPath)
                {
                    Debug.Log("win");
                    gameObject.SetActive(false);
                    
                }
                else
                {
                    Debug.Log("loose (" + pointDifferent.name + "/" + modelDifferent.name + ")");
                }
            }
            
            trailRenderer.Clear();
            trailRenderer.gameObject.SetActive(false);
            _points.Clear();
            Debug.Log("cleaaar ");
            canDraw = false;
        }
    }
    
}
