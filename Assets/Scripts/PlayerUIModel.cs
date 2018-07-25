using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIModel : MonoBehaviour
{
    public PlayerInput.PlayerId playerId = PlayerInput.PlayerId.pad_1;
    public SkinnedMeshRenderer _skinnedMesh;
    public Material[] _materials;

    public Text score;

    void Start()
    {
        if (CharacterPanel.activePlayers != null && CharacterPanel.activePlayers[transform.GetSiblingIndex()] == PlayerInput.PlayerId.none)
        {
            gameObject.SetActive(false);
            score.gameObject.SetActive(false);
            return;
        }

        _skinnedMesh.material = _materials[transform.GetSiblingIndex()];
        score.gameObject.SetActive(true);
        score.text = ScoreManager.GetPoints(transform.GetSiblingIndex()).ToString();      
    }
}