using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_music : MonoBehaviour
{

    [SerializeField] private AudioClip sontransiboss = null;
    public AudioSource main,sfx;
    private bool bossverif = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._instance.IsInBoss)
        {
            main.Stop();
            bossverif = true;
        }
        if (!GameManager._instance.IsInBoss && bossverif)
        {
            bossverif = false;
            main.Play();
            
        }

    }

    public void Eat()
    {
        sfx.Play();
    }
}
