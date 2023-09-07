using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreboardText;

    [SerializeField] private int score;

    public int Score
    {
        get => score;
        set
        {
            score = value;
            scoreText.text = score.ToString();
        }
    }

    private Dictionary<string, int> playersScore = new Dictionary<string, int>();



    private string[] names = { "Amaury", "Ellie", "Bernabé","Flo" };

    private int index;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (index >= names.Length)
            {
                return;
            }
            
            OnSaveData(names[index],Random.Range(0,100));
            index++;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            DrawData();
        }
    }

    public void OnSaveData(string name,int score)
    {
        playersScore.Add(name,score);
        Debug.Log("add score");
    }

    public void DrawData()
    {
        playersScore = playersScore.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

        string text = "";
        
        foreach (string playerName in playersScore.Keys)
        {
            string color = "";
            int rank = (playersScore.Keys.ToList().IndexOf(playerName) + 1);

            switch (rank)
            {
                case 1:
                    color = "<color=yellow> ";
                    break;
                
                case 2:
                    color = "<color=#CCCCCC> ";
                    break;
                
                case 3:
                    color = "<color=#663300> ";
                    break;
                
                default:
                    color = "<color=white> ";
                    break;
            }
            
            text += color + (playersScore.Keys.ToList().IndexOf(playerName) + 1) + "." + " " + playerName + " ....................................... " + playersScore[playerName] + "</color> \n";
        }

        scoreboardText.text = text;
    }
}
