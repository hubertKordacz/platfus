using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIModel : MonoBehaviour
{
    public PlayerInput.PlayerId playerId = PlayerInput.PlayerId.player1;
    public SkinnedMeshRenderer _skinnedMesh;
    public Material[] _materials;

    public Text score;

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
                    score.gameObject.SetActive(true);
                    score.text = "0";
                    return;
                }
            }

            gameObject.SetActive(false);
            score.gameObject.SetActive(false);
        }
    }
}
