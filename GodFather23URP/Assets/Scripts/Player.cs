using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{

    private void OnCollisionStay2D(Collision2D col)
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (col.gameObject.TryGetComponent(out BossManager bossManager) && !GameManager._instance.IsInBoss)
            {
                bossManager.music.clip = bossManager.musics[0];
                bossManager.music.Play();
                StartCoroutine(WaitMusic(bossManager));
                bossManager.OnFormBegin?.Invoke(this,new BossManager.OnFormBeginArgs());    
            }
            
        }
    }

    private IEnumerator WaitMusic(BossManager bossManager)
    {
        yield return new WaitForSeconds(0.5f);
        bossManager.music.clip = bossManager.musics[1];
        bossManager.music.Play();
    }

}
