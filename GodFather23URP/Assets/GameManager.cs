using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Serializable]
    public class ComboMultiplier
    {
        [SerializeField] private int insect;
        [SerializeField] private float comboMultiplier;

        public int Insect
        {
            get => insect;
            set => insect = value;
        }

        public float Combo
        {
            get => comboMultiplier;
            set => comboMultiplier = value;
        }
    }
    
    
    public static GameManager _instance;
    public List<GameObject> _listMonster;

    private bool _isInBoss;

    [SerializeField] private int pointEat;

    public bool IsInBoss
    {
        get => _isInBoss;
        set => _isInBoss = value;
    }
    
    
    private void Awake()
    {
        _instance = this;
    }

    [SerializeField] private List<ComboMultiplier> combos;

    //quand on fini un cercle complet
    public void WebIsFinish()
    {
        int _multiplier = 0;
        for(int _loop = 0; _loop < _listMonster.Count; _loop++)
        {
            bool _combo = _listMonster[_loop].GetComponent<timerDead>().DoIMDead();
            if (_combo)
                _multiplier++;
        }


        ComboMultiplier combo = combos.Where(combo => combo.Insect == _multiplier).FirstOrDefault();
        float comboMultiplier = combo == null ? combos.Last().Combo : combo.Combo;

// Probleme lorsqu'on ferme mal ca ajoute du score pour rien 
        FindObjectOfType<ScoreManager>().Score =FindObjectOfType<ScoreManager>().Score + ((int)((pointEat * _multiplier) * comboMultiplier));

        Debug.Log("combo de : " + _multiplier + " insectes");
    }

}
