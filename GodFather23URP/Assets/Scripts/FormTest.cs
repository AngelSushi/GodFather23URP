using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class FormTest : MonoBehaviour
{

    [SerializeField] private bool canDraw;

    [SerializeField] private List<GameObject> models = new List<GameObject>();
    
    private List<GameObject> _points = new List<GameObject>();

    [SerializeField] public TrailRenderer trailRenderer;

    private bool _succeed;
    private void Awake()
    {
        for (int i = 0; i < transform.parent.parent.childCount - 1; i++)
        {
            models.Add(transform.parent.parent.GetChild(i).gameObject);
        }
        
        Debug.Log("modelLength " + models.Count);

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

    private void OnMouseDown()
    {
        if (canDraw)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 4;
            trailRenderer.transform.position = worldPosition;
            trailRenderer.SetPosition(0,worldPosition);    
        }
    }

    private void OnMouseUp()
    {
        if (canDraw)
        {
            if (_points.Count != models.Count)
            {
                Debug.Log("loose");
                BossManager boss = FindObjectsOfType<BossManager>().First(boss => boss.IsInFight);

                boss.sound.clip = boss.sounds[0];
                boss.sound.Play();
            }
            else
            {   
                bool goodPath = true;
                int modelCount = models.Count;
                int pointCOunt = _points.Count;
                
                for (int i = 0; i < _points.Count; i++)
                {
                    if (_points[i] != models[i])
                    {
                        goodPath = false;
                        break;
                    }
                }

                if (goodPath)
                {
                    Debug.Log("win");
                    transform.parent.parent.gameObject.SetActive(false);
                    BossManager boss = FindObjectsOfType<BossManager>().First(boss => boss.IsInFight);

                    boss.OnFormFinished?.Invoke(this,new BossManager.OnFormFinishedArgs(gameObject));

                }
                else
                {
                    Debug.Log("loose");
                    BossManager boss = FindObjectsOfType<BossManager>().First(boss => boss.IsInFight);
                    boss.sound.clip = boss.sounds[0];
                    boss.sound.Play();
                }
            }
            
            trailRenderer.Clear();
            trailRenderer.gameObject.SetActive(false);
            _points.Clear();
            canDraw = true;
        }
    }
    
}
