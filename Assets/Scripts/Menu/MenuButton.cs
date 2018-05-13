using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public Sprite normal, highlight;

    public MenuButton up, down, right, left;
    public Image target;

    public void OnClick()
    {

    }

    public void OnSellect()
    {
        if (target) target.sprite = highlight;
    }

    public void OnDiselect()
    {
        if (target) target.sprite = normal;
    }

    public MenuButton OnRight()
    {
        return right;
    }

    public MenuButton Onleft()
    {
        return left;
    }

    public MenuButton OnUp()
    {
        return up;
    }

    public MenuButton OnDown()
    {
        return down;
    }

    //private void OnGUI()
    //{
    //    if (Sellection.Current)
    //        GUI.Label(new Rect(25, 25, 200, 25), Sellection.Current.gameObject.name);
    //}
}

public static class Sellection
{
    static MenuButton _current;
    public static MenuButton Current { get { return _current; } }

    public static void SellectButton(MenuButton button)
    {
        if (_current) _current.OnDiselect();
        _current = button;
        if (_current) _current.OnSellect();
    }
}
