using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MenuPanel
{
    public static List<PlayerInput.PlayerId> players;

    public Text[] texts;
    public Material[] materials;
    public PlayerInput[] models;

    public GameObject readyText;
    public Image fader;

    bool[] _ready;
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

    public override void OnConfirm(int player)
    {
        if (!_waitForFade)
        {
            if (player == 0)
            {
                if (AllReady())
                {
                    _waitForFade = true;
                    ScoreManager.Instance.ResetGame();
                    return;
                }
            }

            if (!players.Contains((PlayerInput.PlayerId)player))
            {
                players.Add((PlayerInput.PlayerId)player);

                models[player].gameObject.SetActive(true);
                models[player]._skinnedMesh.material = materials[players.IndexOf((PlayerInput.PlayerId)player)];
                texts[player].gameObject.SetActive(false);

                readyText.gameObject.SetActive(false);
            }
            else
            {
                var index = players.IndexOf((PlayerInput.PlayerId)player);
                _ready[index] = true;

                texts[player].gameObject.SetActive(true);
                texts[player].text = "Ready";

                if (AllReady()) readyText.gameObject.SetActive(true);
            }
        }
    }

    public override void OnCancel(int player)
    {
        if (player == 0 && !_ready[0])
        {
            MainMenu.Instance.ShowPannel(MainMenu.Menu.main);
            return;
        }

        if (players.Contains((PlayerInput.PlayerId)player))
        {
            if (_ready[players.IndexOf((PlayerInput.PlayerId)player)])
            {
                _ready[players.IndexOf((PlayerInput.PlayerId)player)] = false;

                texts[player].gameObject.SetActive(false);
            }
            else
            {
                players.Remove((PlayerInput.PlayerId)player);

                models[player].gameObject.SetActive(false);
                texts[player].gameObject.SetActive(true);
                texts[player].text = "Join";
            }
        }
    }

    bool AllReady()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (!_ready[i]) return false;
        }

        return true;
    }

    public override void OnShow()
    {
        base.OnShow();

        players = new List<PlayerInput.PlayerId>();
        _ready = new bool[4];

        players.Add(PlayerInput.PlayerId.player1);

        texts[0].gameObject.SetActive(false);
        models[0].gameObject.SetActive(true);

        for (int i = 1; i < 4; i++)
        {
            texts[i].gameObject.SetActive(true);
            texts[i].text = "Join";
            models[i].gameObject.SetActive(false);
        }

        readyText.gameObject.SetActive(false);
    }
}
