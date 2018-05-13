using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerInput : MonoBehaviour
{
    public SkinnedMeshRenderer _skinnedMesh;
    public Material[] _materials;

    public enum PlayerId
    {
        player1 = 0,
        player2 = 1,
        player3 = 2,
        player4 = 3,
        player5 = 4
    }
    public enum InputActions
    {
        Vertical = 0,
        Horizontal = 1,
        Attack = 2
    }

    public PlayerId playerId = PlayerId.player1;

    // Use this for initialization
    void Start()
    {
        if (CharacterPanel.players == null)
        {
            _skinnedMesh.material = _materials[(int)playerId];
        }
        else
        {
            for (int i = 0; i < CharacterPanel.players.Count; i++)
            {
                if (CharacterPanel.players[i] == playerId)
                {
                    _skinnedMesh.material = _materials[i];
                    return;
                }
            }
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (ScoreManager.Instance) ScoreManager.Instance.SetPoints((int)playerId);
    }

    // Update is called once per frame
    //void Update () {

    //Debug.Log("P " + this.playerId + " " +(int)playerId + " "+ rewiredPlayer.name  + " "+ GetAxis(InputActions.Horizontal));
    //}
    private Player rewiredPlayer
    {
        get
        {
            return ReInput.players.GetPlayer((int)playerId);
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
