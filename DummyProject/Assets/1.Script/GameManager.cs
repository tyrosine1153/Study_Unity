using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int goalItemCount;
    public int stage;
    public Text goalItemText;
    public Text countItemText;

    void Start()
    {
        goalItemText.text = "/ " + goalItemCount;
    }

    public void GetItem(int count)
    {
        countItemText.text = count.ToString();
    }
}
