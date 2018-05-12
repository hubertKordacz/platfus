using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Color))]
public class ColorPingPong : MonoBehaviour
{
    public Text text;

    public float maxAlphaCut = 0.5f;
    public float speed = 2;

    void Update()
    {
        var color = text.color;
        color.a = Mathf.PingPong(Time.time, 1 - maxAlphaCut) + maxAlphaCut;
        text.color = color;
    }
}
