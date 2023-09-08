using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] float _coefDetection = .1f;
    Vector2 _A;
    Vector2 _B;
    Vector2 _C;
    Vector2 _D;
    private void Start()
    {
        FormDetection();
    }
    /*
    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if(_collision.tag == "Cobweb")
        {
            //Debug.Log("wire : " + _collision.GetComponent<CobwebScript>()._wire + " checkpoint : " + GetComponent<CobwebScript>()._wire);
            if (GetComponent<CobwebScript>()._wire != _collision.GetComponent<CobwebScript>()._wire &&
                GetComponent<CobwebScript>()._wire != _collision.GetComponent<CobwebScript>()._wire + 1 &&
                GetComponent<CobwebScript>()._wire != _collision.GetComponent<CobwebScript>()._wire - 1)
            {
                GameManager._instance.WebIsFinish();
            }
        }
        //Debug.Log(_collision.GetComponent<CobwebScript>()._wire);

        
    }
    */

    public void FormDetection()
    {
        //AB C & D
        //A = ancien checkpoint
        Vector2 A = GameObject.FindGameObjectWithTag("Player").GetComponent<WebSpawnerp2>()._actualWeb.GetComponent<SpawnCobweb>()._triangles[GameObject.FindGameObjectWithTag("Player").GetComponent<WebSpawnerp2>()._actualWeb.GetComponent<SpawnCobweb>()._triangles.Count - 2].transform.position;

        //B = dernier check point 
        Vector2 B = GameObject.FindGameObjectWithTag("Player").GetComponent<WebSpawnerp2>()._actualWeb.GetComponent<SpawnCobweb>()._triangles[GameObject.FindGameObjectWithTag("Player").GetComponent<WebSpawnerp2>()._actualWeb.GetComponent<SpawnCobweb>()._triangles.Count - 1].transform.position;

        Vector2 AB = new Vector2(B.x - A.x, B.y - A.y);

        Vector2 C = new Vector2(A.x - AB.x * _coefDetection, A.y - AB.y * _coefDetection);

        Vector2 D = new Vector2(B.x + AB.x * _coefDetection, B.y + AB.y * _coefDetection);

        //Debug.Log("A : " + A + " B : " + B + " AB : " + AB + " C : " + C + " D : " + D);

        /*
        _A = A;
        _B = B;
        _C = C;
        _D = D;
        */

        Vector2 origin = C;

        Vector3 _direction = D - C;
        _direction.z = 0; // Assurez-vous que la direction reste en 2D
        //Debug.Log(_direction);

        // Effectuez le raycast 2D.
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, _direction, new Vector2(D.x - C.x , D.y - C.y).magnitude);

        // Parcourez toutes les collisions d�tect�es.
        foreach (RaycastHit2D hit in hits)
        {
            // V�rifiez si le collider appartient � un autre objet (�vite de d�tecter lui-m�me).
            if (hit.collider != null)
            {
                if(hit.collider.tag == "Cobweb" && hit.collider.GetComponent<CobwebScript>() != null)
                {
                    if (hit.collider.GetComponent<CobwebScript>()._wire !=
                        GameObject.FindGameObjectWithTag("Player").GetComponent<WebSpawnerp2>()._actualWeb.GetComponent<SpawnCobweb>()._triangles[GameObject.FindGameObjectWithTag("Player").GetComponent<WebSpawnerp2>()._actualWeb.GetComponent<SpawnCobweb>()._triangles.Count - 2].GetComponent<CobwebScript>()._wire - 1 &&
                        hit.collider.GetComponent<CobwebScript>()._wire !=
                        GameObject.FindGameObjectWithTag("Player").GetComponent<WebSpawnerp2>()._actualWeb.GetComponent<SpawnCobweb>()._triangles[GameObject.FindGameObjectWithTag("Player").GetComponent<WebSpawnerp2>()._actualWeb.GetComponent<SpawnCobweb>()._triangles.Count - 1].GetComponent<CobwebScript>()._wire - 1 &&
                        hit.collider.GetComponent<CobwebScript>()._wire !=
                        GameObject.FindGameObjectWithTag("Player").GetComponent<WebSpawnerp2>()._actualWeb.GetComponent<SpawnCobweb>()._triangles[GameObject.FindGameObjectWithTag("Player").GetComponent<WebSpawnerp2>()._actualWeb.GetComponent<SpawnCobweb>()._triangles.Count - 1].GetComponent<CobwebScript>()._wire
                       )
                    {
                        GameManager._instance.WebIsFinish();
                        GameObject.FindGameObjectWithTag("Player").GetComponent<WebSpawnerp2>()._actualWeb.GetComponent<SpawnCobweb>()._maxAmountOfCheckpoint = GameObject.FindGameObjectWithTag("Player").GetComponent<WebSpawnerp2>()._actualWeb.GetComponent<SpawnCobweb>()._triangles.Count;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<WebSpawnerp2>()._actualWeb.GetComponent<SpawnCobweb>().SelfDestruct();
                    }

                }


            }
        }
    }

    private void OnDrawGizmos()
    {
        /*
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_C, _D);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(_A, _B);
        */
        //Gizmos.DrawWireSphere(_spider.transform.position, _spawnRange);
    }
}
