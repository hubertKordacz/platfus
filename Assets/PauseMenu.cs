using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public MenuButton[] buttons;

    int _index;

    private void OnEnable()
    {
        _index = 0;
        buttons[0].OnSellect();
        buttons[1].OnDeselect();
    }

    public void OnConfirm()
    {
        Debug.Log("X");

        if (_index == 0)
        {
            SceneManager.Instance.ClosePausePanel();
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }
    }

    public void Navigate(float value)
    {
        buttons[_index].OnDeselect();

        if (value > 0)
            _index = 0;
        else if (value < 0)
            _index = 1;

        buttons[_index].OnSellect();
    }
}
