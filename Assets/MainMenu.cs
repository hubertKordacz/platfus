using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image[] background;
    public float backgroundMoveSpeed;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < background.Length; i++)
        //{
        //    background[i].transform.position += Vector3.left * backgroundMoveSpeed * Time.deltaTime;
        //    if (background[i].rectTransform.anchoredPosition.x < -background[i].rectTransform.sizeDelta.x)
        //    {
        //        background[i].rectTransform.anchoredPosition = new Vector3(background[i].rectTransform.sizeDelta.x, 0, 0);
        //    }
        //}
    }
}
