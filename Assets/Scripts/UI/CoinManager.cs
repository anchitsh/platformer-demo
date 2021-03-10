using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinManager : MonoBehaviour
{
    public int totalcoins;
    public static int coins;
    public Text coinText, totalCoins;
    void Start()
    {
        coins = 0;
        totalCoins.text = "/" + totalcoins.ToString();
    }
    void Update()
    {
        coinText.text = coins.ToString();
        if(coins== totalcoins)
        {
            GetComponent<MainMenu>().coinsCollected = true;
        }
    }

}
