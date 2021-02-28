using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinManager : MonoBehaviour
{
    public int totalcoins;
    public static int coins;
    public Text coinText;
    int temp;
    // Start is called before the first frame update
    void Start()
    {
        coins = 0;

    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = coins.ToString() + "/" + totalcoins.ToString();
    }

}
