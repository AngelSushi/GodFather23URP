using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossManager : MonoBehaviour
{
    [SerializeField] private int life;

    private int maxLife;
    public class OnFormFinishedArgs : EventArgs
    {
        private GameObject _currentForm;

        public GameObject CurrentForm
        {
            get => _currentForm;
            set => _currentForm = value;
        }

        public OnFormFinishedArgs(GameObject currentForm)
        {
            _currentForm = currentForm;
        }
    }

    public EventHandler<OnFormFinishedArgs> OnFormFinished;

    public class OnFormBeginArgs : EventArgs
    {
        
    }

    public EventHandler<OnFormBeginArgs> OnFormBegin;

    
    private List<FormTest> _allForms;
    private FormTest _current;
    
    
    private int _previewIndex;

    private bool _isInFight;

    public bool IsInFight
    {
        get => _isInFight;
        set => _isInFight = true;
    }
    
    
    private void Start()
    {
        OnFormFinished += OnFormFinishedFunc;
        OnFormBegin += OnFormBeginFunc;
        maxLife = life; 
        _allForms = FindObjectsOfType<FormTest>().ToList();
        _allForms.ForEach(form => SetLightEnabled(form.gameObject,false));
    }

    private void OnDisable()
    {
        OnFormFinished -= OnFormFinishedFunc;
        OnFormBegin -= OnFormBeginFunc;
    }

    private void OnFormBeginFunc(object sender, OnFormBeginArgs e)
    {
        Debug.Log("on form begin");
        
        IsInFight = true;
        RandomPattern();
    }

    private void OnFormFinishedFunc(object sender,OnFormFinishedArgs e)
    {
        Debug.Log("on form finished");
        
        if (!IsInFight)
        {
            return;
        }
        
        StopAllCoroutines();
        
        life--;
        Mathf.Clamp(life, 0, maxLife);
        _current = null;

        if (life > 0)
        {
            OnFormBegin.Invoke(this,new OnFormBeginArgs());
        }
        else
        {
           Debug.Log("il est moooort"); 
        }

    }

    public void RandomPattern()
    {
        int random = Random.Range(0, _allForms.Count - 1);
        _current = _allForms[random];
        
        Debug.Log("random " + _current.gameObject.name);
        
        SetLightEnabled(_current.gameObject,true);
        _current.gameObject.SetActive(true);
        GameManager._instance.IsInBoss = true;
        
        _previewIndex = 0;

        for (int i = 0; i < _current.transform.childCount; i++)
        {
            _current.transform.GetChild(_previewIndex).GetComponent<Animator>().enabled = false;
        }
        
        _current.transform.GetChild(_previewIndex).GetComponent<Animator>().enabled = true;

        StartCoroutine(WaitPreviewEnd());
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

    private void SetLightEnabled(GameObject form,bool value)
    {
        for (int i = 0; i < form.transform.childCount; i++)
        {
            form.transform.GetChild(i).gameObject.SetActive(value);
        }
    }
}
