using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSellect : MainPanel
{
    public static int levelToLoad = 1;

    public override void OnConfirm(int player)
    {
        if (Sellection.Current == buttons[0]) ShowStageSelect(0);
        else if (Sellection.Current == buttons[1]) ShowStageSelect(1);
        else if (Sellection.Current == buttons[2]) ShowStageSelect(2);
        else Back();
    }

    void ShowStageSelect(int stage)
    {
        MainMenu.Instance.ShowPannel(MainMenu.Menu.character);
        levelToLoad = stage + 1;
    }

    void Back()
    {
        MainMenu.Instance.ShowPannel(MainMenu.Menu.main);
    }
}
