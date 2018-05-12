using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MenuPanel
{
    public static List<PlayerInput.PlayerId> players;

    public Image[] slots;
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
                    return;
                }
            }

            if (!players.Contains((PlayerInput.PlayerId)player))
            {
                players.Add((PlayerInput.PlayerId)player);
                slots[players.IndexOf((PlayerInput.PlayerId)player)].color = Color.blue;

                readyText.gameObject.SetActive(false);
            }
            else
            {
                var index = players.IndexOf((PlayerInput.PlayerId)player);
                _ready[index] = true;
                slots[index].color = Color.red;

                if (AllReady()) readyText.gameObject.SetActive(true);
            }
        }
    }

    public override void OnCancel(int player)
    {
        if (player == 0 && !_ready[0])
        {
            MainMenu.Instance.ShowPannel(MainMenu.Menu.stage);
            return;
        }

        if (players.Contains((PlayerInput.PlayerId)player))
        {
            if (_ready[players.IndexOf((PlayerInput.PlayerId)player)])
            {
                _ready[players.IndexOf((PlayerInput.PlayerId)player)] = false;
                slots[players.IndexOf((PlayerInput.PlayerId)player)].color = Color.blue;
            }
            else
            {
                players.Remove((PlayerInput.PlayerId)player);
                slots[players.IndexOf((PlayerInput.PlayerId)player)].color = Color.white;
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

        slots[0].color = Color.blue;
        for (int i = 1; i < slots.Length; i++)
        {
            slots[i].color = Color.white;
        }

        readyText.gameObject.SetActive(false);
    }
}
