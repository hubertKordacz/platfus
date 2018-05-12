using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuPanel : MonoBehaviour
{
    public MenuButton[] buttons;

    protected int _index;

    CanvasGroup _canvasGroup;

    protected virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
    }

    public virtual void OnShow()
    {
        _index = 0;
        _canvasGroup.alpha = 1;
        if (buttons.Length > 0)
            Sellection.SellectButton(buttons[0]);
        else
            Sellection.SellectButton(null);
    }

    public virtual void OnHide()
    {
        _canvasGroup.alpha = 0;
    }

    protected virtual void Update()
    {

    }

    public virtual void OnCofirm(int player)
    {

    }

    public virtual bool Vertical(int player, float value)
    {
        if (player > 0) return false;

        if (value > 0.5f && Sellection.Current.up)
        {
            Sellection.SellectButton(Sellection.Current.OnUp());
            return true;
        }
        if (value < -0.5f && Sellection.Current.down)
        {
            Sellection.SellectButton(Sellection.Current.OnDown());
            return true;
        }

        return false;
    }

    public virtual bool Horizontal(int player, float value)
    {
        if (player > 0) return false;

        if (value > 0.5f && Sellection.Current.right)
        {
            Sellection.SellectButton(Sellection.Current.OnRight());
            return true;
        }
        if (value < -0.5f && Sellection.Current.left)
        {
            Sellection.SellectButton(Sellection.Current.Onleft());
            return true;
        }

        return false;
    }
}
