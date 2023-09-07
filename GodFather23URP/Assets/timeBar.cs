using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class timeBar : MonoBehaviour
{

    public int maxtime = 100;
    public int damages = 1;
    public int currentHealth;

    public float timer = 1f;
    private bool damageActive = false;

    public sliderBar slideBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxtime;
        slideBar.SetMaxTime(maxtime);
        StartCoroutine(timeDamage());
    }

    private void Update()
    {
        if (damageActive) 
        {
            damageActive = false;
            StartCoroutine(timeDamage());
            Debug.Log("false");
        }
    }

    public void Regeneratetime(int Regen)
    {
        if (currentHealth < maxtime)
        {
            int healthDifference = maxtime - currentHealth;
            int actualRegeneration = Mathf.Min(healthDifference, Regen);
            currentHealth += actualRegeneration;
            slideBar.SetTime(currentHealth);
            Debug.Log("Regenerated " + actualRegeneration + " time. Current time: " + currentHealth);
        }
        else
        {
            Debug.Log("time est d�ja full. pas besoin de r�generation.");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        slideBar.SetTime(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator timeDamage()
    {
        yield return new WaitForSeconds(timer);
        TakeDamage(damages);
        damageActive = true;
        Debug.Log("true");
    }

    public void Die ()
    {

    }
}
