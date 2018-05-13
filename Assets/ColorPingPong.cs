using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Color))]
public class ColorPingPong : MonoBehaviour
{
    public Image image;

    public float maxAlphaCut = 0.5f;
    public float speed = 2;

    void Update()
    {
        //var color = image.color;
        //color.a = Mathf.PingPong(Time.time, 1 - maxAlphaCut) + maxAlphaCut;
        //image.color = color;
    }
}
