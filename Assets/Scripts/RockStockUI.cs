using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockStockUI : MonoBehaviour
{
    public Text CounterText;
    public int NbRock = 0;
    public int NbMaxRock = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CounterText.text = NbRock.ToString() + " / " + NbMaxRock.ToString();
    }
}
