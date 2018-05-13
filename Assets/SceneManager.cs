using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public Image image;

    public PlayerController[] players;

    public static bool secondRound = false;

    bool _loading, _fading = false;
    float _timer = 0;

    public void Update()
    {
        if (_fading)
        {
            _timer += Time.deltaTime * 2;
            var color = image.color;
            color.a = Mathf.Lerp(0, 1, _timer);
            image.color = color;

            return;
        }

        if (_loading) return;

        int alive = 0;
        int aliveIndex = 0;

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].Equals(null)) continue;
            if (players[i].gameObject && !players[i].gameObject.activeInHierarchy || players[i].die) continue;

            alive++;
            aliveIndex = i;
        }

        if (alive > 1) return;


        players[aliveIndex].playerInput.SetPoints();
        StartCoroutine(NextLevel());
    }

    IEnumerator NextLevel()
    {
        _loading = true;

        yield return new WaitForSeconds(0.5f);

        _fading = true;

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
