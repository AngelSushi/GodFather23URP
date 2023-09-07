using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class timerDead : MonoBehaviour
{
    public float timer;


    void Start()
    {
        StartCoroutine(endTimer());
    }

    private IEnumerator endTimer()
    {
        yield return new WaitForSeconds(timer);
        FindObjectOfType<spawn>().spawnList.Remove(transform.position);
        Destroy(gameObject);
    }

    public void restartTimer ()
    {
        StopCoroutine(endTimer());
        StartCoroutine(endTimer());
    }

    public bool DoIMDead()
    {
        Vector2 origin = transform.position;
        int _side = 0;
        // Calculez la direction du rayon vers la gauche (n�gatif de l'axe X).
        Vector2 _directionLeft = Vector2.left;
        Vector2 _directionRight = Vector2.right;
        Vector2 _directionUp = Vector2.up;
        Vector2 _directionDown = Vector2.down;

        // Effectuez le raycast 2D.
        RaycastHit2D[] _hitsLeft = Physics2D.RaycastAll(origin, _directionLeft, 100, 1 << 7);
        RaycastHit2D[] _hitsRight = Physics2D.RaycastAll(origin, _directionRight, 100, 1 << 7);
        RaycastHit2D[] _hitsUp = Physics2D.RaycastAll(origin, _directionUp, 100, 1 << 7 );
        RaycastHit2D[] _hitsDown = Physics2D.RaycastAll(origin, _directionDown, 100, 1 << 7);

        // Parcourez toutes les collisions d�tect�es.
        foreach (RaycastHit2D hit in _hitsLeft)
        {
            // V�rifiez si le collider appartient � un autre objet (�vite de d�tecter lui-m�me).
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                Debug.Log(hit.collider.gameObject.layer);
                Debug.Log("Objet � gauche : " + hit.collider.gameObject.name);
                _side++;
                break;
            }
        }
        foreach (RaycastHit2D hit in _hitsRight)
        {
            // V�rifiez si le collider appartient � un autre objet (�vite de d�tecter lui-m�me).
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                Debug.Log("Objet � droite : " + hit.collider.gameObject.name);
                _side++;
                break;
            }
        }
        foreach (RaycastHit2D hit in _hitsUp)
        {
            // V�rifiez si le collider appartient � un autre objet (�vite de d�tecter lui-m�me).
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                Debug.Log("Objet � haut : " + hit.collider.gameObject.name);
                _side++;
                break;
            }
        }
        foreach (RaycastHit2D hit in _hitsDown)
        {
            // V�rifiez si le collider appartient � un autre objet (�vite de d�tecter lui-m�me).
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                Debug.Log("Objet � bas : " + hit.collider.gameObject.name);
                _side++;
                break;
            }
        }

        if (_side == 4)
            return true;


        return false;
    }

}
