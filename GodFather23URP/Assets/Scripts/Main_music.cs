using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_music : MonoBehaviour
{

    [SerializeField] private AudioClip sontransiboss = null;
    private AudioSource audiosource_transiboss;
    private bool bossverif = false;

    // Start is called before the first frame update
    void Start()
    {
        audiosource_transiboss = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._instance.IsInBoss)
        {
            audiosource_transiboss.Stop();
            audiosource_transiboss.PlayOneShot(sontransiboss);
            bossverif = true;
        }
        if (!GameManager._instance.IsInBoss && bossverif)
        {

        }

    }
}
