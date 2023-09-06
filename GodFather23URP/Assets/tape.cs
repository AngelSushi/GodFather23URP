using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tape : MonoBehaviour
{
    public bool canAttackAgain = true;
    public float cooldown = 3f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && canAttackAgain)
        {
            Debug.Log("Tape");
            attack();
            canAttackAgain = false;
            StartCoroutine (EndCooldown());
        }
    }

    public void attack () { 
        
    }

    private IEnumerator EndCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canAttackAgain = true;
    }
}
