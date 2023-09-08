using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
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

    
    private FormTest _current;
    
    
    private int _previewIndex;

    private bool _isInFight;

    public bool IsInFight
    {
        get => _isInFight;
        set => _isInFight = true;
    }

    public List<AudioClip> musics;
    public List<AudioClip> sounds;

    public AudioSource music;
    public AudioSource sound;
    private void Start()
    {
        OnFormFinished += OnFormFinishedFunc;
        OnFormBegin += OnFormBeginFunc;
        maxLife = life; 
    }

    private void OnDisable()
    {
        OnFormFinished -= OnFormFinishedFunc;
        OnFormBegin -= OnFormBeginFunc;
    }

    private void OnFormBeginFunc(object sender, OnFormBeginArgs e)
    {

        IsInFight = true;
        RandomPattern();
    }

    private void OnFormFinishedFunc(object sender,OnFormFinishedArgs e)
    {
        
        if (!IsInFight)
        {
            return;
        }
        
        StopAllCoroutines();
        
        life--;

        Debug.Log("size " + _current.transform.parent.GetChild(4).gameObject.name);
        
        GetComponent<Animator>().SetInteger("State",life);

        Mathf.Clamp(life, 0, maxLife);
        _current = null;

        if (life > 0)
        {
            OnFormBegin.Invoke(this,new OnFormBeginArgs());
            sound.clip = sounds[1]; // sounds
            sound.Play();
        }
        else
        {
            music.clip = musics[2]; // musique
            music.Play();
            Destroy(FindObjectOfType<Player>().collideBoss);
            Debug.Log("il est moooort"); 
        }

    }

    public void RandomPattern()
    {
        int random = Random.Range(0, GameManager._instance._allForms.Count - 1);
        _current = GameManager._instance._allForms[random];
        
        SetLightEnabled(_current.gameObject,true);
        _current.transform.parent.parent.gameObject.SetActive(true);
        _current.transform.parent.parent.GetChild(_current.transform.parent.parent.childCount -1).gameObject.SetActive(true);
        GameManager._instance.IsInBoss = true;


        _current.trailRenderer.transform.position = _current.transform.parent.parent.GetChild(0).position;
        
        _previewIndex = 0;

        for (int i = 0; i < _current.transform.childCount; i++)
        {
            _current.transform.parent.parent.GetChild(_previewIndex).GetComponent<Animator>().enabled = false;
        }
        
        _current.transform.parent.parent.GetChild(_previewIndex).GetComponent<Animator>().enabled = true;

        StartCoroutine(WaitPreviewEnd());
    }
    
    private IEnumerator WaitPreviewEnd()
    {
        yield return new WaitForSeconds(1.5f);

        if (_current != null)
        {
            _current.transform.parent.parent.GetChild(_previewIndex).GetComponent<Animator>().enabled = false;
            _current.transform.parent.parent.GetChild(_previewIndex).GetComponent<Light2D>().pointLightOuterRadius = 0f;
            _previewIndex++;

            if (_previewIndex >= _current.transform.parent.parent.childCount - 1)
            {
                _previewIndex = 0;
            }
            
            _current.transform.parent.parent.GetChild(_previewIndex).GetComponent<Animator>().enabled = true;
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
