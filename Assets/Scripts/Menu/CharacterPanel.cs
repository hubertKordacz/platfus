using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterPanel : MenuPanel
{
    //public class Player
    //{
    //    public PlayerInput.PlayerId player;
    //    public Image join, ready;
    //}

    public static PlayerInput.PlayerId[] activePlayers;

    public Image[] join;
    public Material[] materials;
    public PlayerInput[] models;

    public GameObject readyText;
    public Image fader;

    bool _waitForFade;
    float _fadeTimer;

    protected override void Update()
    {
        base.Update();

        if (!_waitForFade) return;

        _fadeTimer += Time.deltaTime * 2;
        var color = fader.color;
        color.a = Mathf.Lerp(0, 1, _fadeTimer);
        fader.color = color;

        if (_fadeTimer >= 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(StageSellect.levelToLoad);
        }
    }

    public override void OnConfirm(int playerID)
    {
        if (!_waitForFade)
        {
            if (!ContainPlayer((PlayerInput.PlayerId)playerID))
            {
                for (int i = 0; i < activePlayers.Length; i++)
                {
                    if (activePlayers[i] != PlayerInput.PlayerId.none) continue;
                    activePlayers[i] = (PlayerInput.PlayerId)playerID;
                    models[i].gameObject.SetActive(true);
                    models[i]._skinnedMesh.material = materials[i];
                    join[i].gameObject.SetActive(false);
                    readyText.gameObject.SetActive(false);
                    return;
                }
            }
            else if (AllowGameStart())
            {
                _waitForFade = true;
                ScoreManager.ResetGame();
                return;
            }
        }
    }

    public override void OnCancel(int playerID)
    {
        if (ContainPlayer((PlayerInput.PlayerId)playerID))
        {
            for (int i = 0; i < activePlayers.Length; i++)
            {
                if (activePlayers[i] == (PlayerInput.PlayerId)playerID)
                {
                    activePlayers[i] = PlayerInput.PlayerId.none;
                    models[i].gameObject.SetActive(false);
                    join[i].gameObject.SetActive(true);
                    return;
                }
            }
        }
        else if (!AllSlotAssigned())
        {
            MainMenu.Instance.ShowPannel(MainMenu.Menu.main);
        }
    }

    public override void OnShow()
    {
        base.OnShow();

        activePlayers = new PlayerInput.PlayerId[4];

        for (int i = 0; i < 4; i++)
        {
            join[i].gameObject.SetActive(true);
            models[i].gameObject.SetActive(false);
        }

        readyText.gameObject.SetActive(false);
    }

    bool ContainPlayer(PlayerInput.PlayerId player)
    {
        for (int i = 0; i < activePlayers.Length; i++)
        {
            if (activePlayers[i] == player) return true;
        }
        return false;
    }

    bool AllowGameStart()
    {
        int count = 0;
        for (int i = 0; i < activePlayers.Length; i++)
        {
            if (activePlayers[i] != PlayerInput.PlayerId.none) count++;
            if (count >= 2) return true;
        }
        return false;
    }

    bool AllSlotAssigned()
    {
        int count = 0;
        for (int i = 0; i < activePlayers.Length; i++)
        {
            if (activePlayers[i] != PlayerInput.PlayerId.none) count++;
        }
        return count == 4;
    }
}
