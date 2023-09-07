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
        if (Input.GetKeyDown(KeyCode.R) && col.gameObject.TryGetComponent(out BossManager bossManager))
        {
            bossManager.OnFormBegin?.Invoke(this,new BossManager.OnFormBeginArgs());
        }
    }

    

}
