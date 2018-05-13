using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public PlayerController[] players;

    bool _loading = false;

    public void Update()
    {
        if (_loading) return;

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].Equals(null)) continue;
            if (!players[i].Equals(null) && players[i].gameObject && players[i].gameObject.activeInHierarchy)
                return;
        }

        _loading = true;
        StartCoroutine(NextLevel());
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1);

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
