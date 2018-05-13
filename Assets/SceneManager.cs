using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public PlayerController[] players;

    public static bool secondRound = false;

    bool _loading = false;

    public void Update()
    {
        if (_loading) return;

        int alive = 0;

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].Equals(null)) continue;
            if (players[i].gameObject && !players[i].gameObject.activeInHierarchy) continue;

            alive++;
        }

        if (alive > 1) return;

        _loading = true;
        StartCoroutine(NextLevel());
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1);

        StageSellect.levelToLoad++;

        if (!secondRound && StageSellect.levelToLoad == 4)
        {
            secondRound = true;
            StageSellect.levelToLoad = 1;
        }

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
