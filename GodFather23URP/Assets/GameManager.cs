using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager _instance;
    public List<GameObject> _listMonster;

    private bool _isInBoss;

    public bool IsInBoss
    {
        get => _isInBoss;
        set => _isInBoss = value;
    }
    
    
    private void Awake()
    {
        _instance = this;
    }

    //quand on fini un cercle complet
    public void WebIsFinish()
    {

    }

}
