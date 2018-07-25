using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : MenuPanel
{
    public override void OnConfirm(int player)
    {
        if (Sellection.Current == buttons[0]) ShowCharacterSelect();
        else QuitGame();
    }

    void ShowCharacterSelect()
    {
        MainMenu.Instance.ShowPannel(MainMenu.Menu.character);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
