using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public PlayerUIModel[] players = new PlayerUIModel[4];

    public static int[] score = new int[4];
    static int _pointsCounter;

    private void Awake()
    {
        Instance = this;
    }

    public void SetPoints(int player)
    {
        _pointsCounter++;
        score[player] += _pointsCounter;
        if (players[player] && players[player].score) players[player].score.text = score[player].ToString();
    }

    public void ResetGame()
    {
        _pointsCounter = 0;
        for (int i = 0; i < score.Length; i++)
        {
            score[i] = 0;
            players[i].score.text = "0";
        }
    }

}
