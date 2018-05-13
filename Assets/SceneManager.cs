using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    PlayerController[] players;

    public void Update()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != null || players[i].gameObject.activeInHierarchy)
                return;

        }
        StageSellect.levelToLoad++;
        if (StageSellect.levelToLoad < 4)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(StageSellect.levelToLoad);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
