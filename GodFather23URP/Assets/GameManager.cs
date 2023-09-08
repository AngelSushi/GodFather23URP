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

    [System.Serializable]
    public class PatrolChecker
    {
        public GameObject firstTarget;
        public GameObject secondTarget;

        public PatrolChecker(GameObject firstTarget, GameObject secondTarget)
        {
            this.firstTarget = firstTarget;
            this.secondTarget = secondTarget;
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

    public GameObject deadAnim;
    
    private void Awake()
    {
        _instance = this;
    }

    [SerializeField] private List<ComboMultiplier> combos;

    public List<PatrolChecker> patrolCheckers = new List<PatrolChecker>();

    //quand on fini un cercle complet
    public void WebIsFinish()
    {
        int _multiplier = 0;
        for(int _loop = 0; _loop < _listMonster.Count; _loop++)
        {
            bool _combo = _listMonster[_loop].GetComponent<timerDead>().DoIMDead();
            if (_combo)
            {
                _multiplier++;
                

                GameObject deadAnimInstance = Instantiate(deadAnim);
                deadAnimInstance.transform.position = _listMonster[_loop].transform.position;
                deadAnimInstance.transform.localScale = _listMonster[_loop].transform.localScale;
                deadAnimInstance.GetComponent<SpriteRenderer>().sprite = _listMonster[_loop].GetComponent<SpriteRenderer>().sprite;

                Destroy(_listMonster[_loop].gameObject);
                _listMonster.Remove(_listMonster[_loop]);
            }
        }

// Probleme lorsqu'on ferme mal ca ajoute du score pour rien 

        ComboMultiplier combo = combos.FirstOrDefault(combo => combo.Insect == _multiplier);
        float comboMultiplier = combo == null ? combos.Last().Combo : combo.Combo;

        FindObjectOfType<ScoreManager>().Score =FindObjectOfType<ScoreManager>().Score + ((int)((pointEat * _multiplier) * comboMultiplier));

        
        
        
        Debug.Log("combo de : " + _multiplier + " insectes");
    }

}
