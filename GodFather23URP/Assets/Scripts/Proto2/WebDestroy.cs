using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebDestroy : MonoBehaviour
{
    Animator _anim;
    [SerializeField] float _delay = .15f;
    void Start()
    {
        _anim = GetComponent<Animator>();
        
    }
    public void DestroyTheWeb()
    {
        _anim.SetBool("Destroy", true);
        Destroy(gameObject, _delay);
    }
}
