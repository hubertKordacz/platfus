using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerInput : MonoBehaviour
{
    public SkinnedMeshRenderer _skinnedMesh;
    public Material[] _materials;

    int _scoreID;

    public enum PlayerId
    {
        none = 0,
        pad_1 = 1,
        pad_2 = 2,
        pad_3 = 3,
        pad_4 = 4,
        keyboard_1 = 5,
        keyboard_2 = 6,
        test = 7
    }
    public enum InputActions
    {
        Vertical = 0,
        Horizontal = 1,
        Attack = 2,
        Cancel = 3,
        Menu = 4
    }

    public PlayerId playerId = PlayerId.none;

    public void InitializePlayer(int index)
    {
        _skinnedMesh.material = _materials[index];
        _scoreID = index;
    }

    private void OnDestroy()
    {

    }

    public void SetPoints()
    {
        //if (ScoreManager.Instance) ScoreManager.Instance.SetPoints((int)playerId);
        if (ScoreManager.Instance) ScoreManager.Instance.SetPoints(_scoreID);
    }

    private Player rewiredPlayer
    {
        get
        {
            return ReInput.players.GetPlayer((int)playerId - 1);
        }
    }
    public bool GetButtonDown(InputActions input)
    {
        return rewiredPlayer.GetButtonDown(input.ToString());
    }

    public bool GetButtonUp(InputActions input)
    {
        return rewiredPlayer.GetButtonUp(input.ToString());
    }

    public bool GetButton(InputActions input)
    {
        return rewiredPlayer.GetButton(input.ToString());
    }

    public float GetAxis(InputActions input)
    {
        return rewiredPlayer.GetAxis(input.ToString());
    }
}
