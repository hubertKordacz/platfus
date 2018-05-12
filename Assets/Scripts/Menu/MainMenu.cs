using UnityEngine;
using System.Collections;
using Rewired;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance { get; private set; }

    public MenuPanel main, character, stage;

    MenuPanel _current;

    public enum Menu
    {
        main,
        character,
        stage
    }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _current = main;
        main.OnShow();
    }

    void Update()
    {
        if (_block) return;

        var players = ReInput.players;
        for (int i = 0; i < 4; i++)
        {
            if (players.GetPlayer(i).GetButtonDown(PlayerInput.InputActions.Attack.ToString()))
            {
                _current.OnCofirm(i);
            }

            if (Sellection.Current == null) return;

            if (_current.Vertical(i, players.GetPlayer(i).GetAxisRaw(PlayerInput.InputActions.Vertical.ToString())))
                StartCoroutine(BlockInput());
            if (_current.Horizontal(i, players.GetPlayer(i).GetAxisRaw(PlayerInput.InputActions.Horizontal.ToString())))
                StartCoroutine(BlockInput());
        }
    }

    public void ShowPannel(Menu menu)
    {
        _current.OnHide();

        switch (menu)
        {
            case Menu.main:
                _current = main;
                break;

            case Menu.character:
                _current = character;
                break;

            case Menu.stage:
                _current = stage;
                break;
        }

        _current.OnShow();
    }

    bool _block;
    IEnumerator BlockInput()
    {
        _block = true;
        yield return new WaitForSeconds(0.15f);
        _block = false;
    }
}