using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public static int[] score = new int[4];

    public PlayerUIModel[] players = new PlayerUIModel[4];

    static int _pointsCounter;

    private void Awake()
    {
        Instance = this;
        _pointsCounter = 0;
    }

    public void SetPoints(int player)
    {
        _pointsCounter++;
        score[player] += _pointsCounter;
        if (players[player] && players[player].score) players[player].score.text = score[player].ToString();
    }

    public static int GetPoints(int player)
    {
        return score[player];
    }

    public static void ResetGame()
    {
        _pointsCounter = 0;
        for (int i = 0; i < score.Length; i++)
        {
            score[i] = 0;
        }
    }

}
