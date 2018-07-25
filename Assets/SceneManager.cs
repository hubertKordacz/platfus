using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    static SceneManager _instance;
    public static SceneManager Instance { get { return _instance; } }

    public static bool secondRound = false;

    public Image image;
    public PauseMenu pauseMenu;
    public PlayerController[] players;

    bool _loading, _fading = false;
    float _timer = 0;

    public static bool GamePaused { get { return _instance.pauseMenu.gameObject.activeInHierarchy; } }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        ClosePausePanel();

        if (CharacterPanel.activePlayers == null)
        {
            for (int i = 0; i < 4; i++)
            {
                if (i == 0) players[i].playerInput.playerId = PlayerInput.PlayerId.keyboard_1;
                else if (i == 1) players[i].playerInput.playerId = PlayerInput.PlayerId.keyboard_2;
                else if (i == 2) players[i].playerInput.playerId = PlayerInput.PlayerId.pad_1;
                else if (i == 3) players[i].playerInput.playerId = PlayerInput.PlayerId.pad_2;

                players[i].playerInput.InitializePlayer(i);
            }
            return;
        }

        for (int i = 0; i < CharacterPanel.activePlayers.Length; i++)
        {
            if (CharacterPanel.activePlayers[i] == PlayerInput.PlayerId.none)
            {
                players[i].gameObject.SetActive(false);
            }
            else
            {
                players[i].playerInput.playerId = CharacterPanel.activePlayers[i];
                players[i].playerInput.InitializePlayer(i);
            }
        }
    }

    public void Update()
    {
        if (GamePaused) return;

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

    public void OpenPausePanel()
    {
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClosePausePanel()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
