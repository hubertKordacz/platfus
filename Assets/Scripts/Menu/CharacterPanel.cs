using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MenuPanel
{
    public List<PlayerInput.PlayerId> players;
    public bool[] ready;

    public Image[] slots;

    public override void OnConfirm(int player)
    {
        if (player == 0)
        {
            if (AllReady())
            {
                Debug.Log("Start");
                return;
            }
        }

        if (!players.Contains((PlayerInput.PlayerId)player))
        {
            players.Add((PlayerInput.PlayerId)player);
            slots[players.IndexOf((PlayerInput.PlayerId)player)].color = Color.blue;
        }
        else
        {
            var index = players.IndexOf((PlayerInput.PlayerId)player);
            ready[index] = true;
            slots[index].color = Color.red;
        }
    }

    public override void OnCancel(int player)
    {
        if (player == 0 && !ready[0])
        {
            MainMenu.Instance.ShowPannel(MainMenu.Menu.stage);
            return;
        }

        if (players.Contains((PlayerInput.PlayerId)player))
        {
            if (ready[players.IndexOf((PlayerInput.PlayerId)player)])
            {
                ready[players.IndexOf((PlayerInput.PlayerId)player)] = false;
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
            if (!ready[i]) return false;
        }

        return true;
    }

    public override void OnShow()
    {
        base.OnShow();

        players = new List<PlayerInput.PlayerId>();
        ready = new bool[4];

        players.Add(PlayerInput.PlayerId.player1);

        slots[0].color = Color.blue;
        for (int i = 1; i < slots.Length; i++)
        {
            slots[i].color = Color.white;
        }
    }
}
