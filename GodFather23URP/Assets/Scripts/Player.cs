using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{

    public GameObject collideBoss;
    
    private void OnCollisionStay2D(Collision2D col)
    {
        if (Input.GetMouseButton(0))
        {
            if (col.gameObject.TryGetComponent(out BossManager bossManager) && !GameManager._instance.IsInBoss)
            {
                collideBoss = col.gameObject;
                bossManager.music.clip = bossManager.musics[0];
                bossManager.music.Play();
                StartCoroutine(WaitMusic(bossManager));
                bossManager.OnFormBegin?.Invoke(this,new BossManager.OnFormBeginArgs());    
            }
            
        }

     /*   if (Input.GetMouseButton(2) && GameManager._instance.IsInBoss)
        {
            if (col.gameObject.TryGetComponent(out BossManager bossManager))
            {
                
                GameManager._instance.IsInBoss = false;
                bossManager._current.gameObject.transform.parent.parent.gameObject.SetActive(false);
            }


        }
        */
    }

    private IEnumerator WaitMusic(BossManager bossManager)
    {
        yield return new WaitForSeconds(0.5f);
        bossManager.music.clip = bossManager.musics[1];
        bossManager.music.Play();
    }

}
