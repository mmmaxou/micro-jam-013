using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockStockUI : MonoBehaviour
{
    public Text CounterText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CounterText.text = GameManager.Instance.NbRock.ToString() + " / " + GameManager.Instance.MaxNbRock.ToString();
    }
}
