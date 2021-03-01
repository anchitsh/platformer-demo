using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinManager : MonoBehaviour
{
    public int totalcoins;
    public static int coins;
    public Text coinText;
    void Start()
    {
        coins = 0;
    }
    void Update()
    {
        coinText.text = coins.ToString() + "/" + totalcoins.ToString();
    }

}
