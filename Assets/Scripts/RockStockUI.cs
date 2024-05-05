using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RockStockUI : MonoBehaviour
{
    public Text CounterText;
    public Image Bar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CounterText.text = GameManager.Instance.NbRock.ToString() + " / " + GameManager.Instance.MaxNbRock.ToString();
        // Debug.Log(GameManager.Instance.NbRock / (float)GameManager.Instance.MaxNbRock);
        Bar.fillAmount = GameManager.Instance.NbRock / (float)GameManager.Instance.MaxNbRock;
        // Bar.fillAmount = 0.5f;
        // Bar.fillAmount = (float)Math.Cos(Time.time) * 0.5f + 0.5f;
    }
}
