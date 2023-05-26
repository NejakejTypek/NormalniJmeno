using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coins : MonoBehaviour
{
    public int[] playerCoins;
    public TextMeshProUGUI[] coinsText;

    private void Update()
    {
        coinsText[0].text = "Coins: " + playerCoins[0];
        coinsText[1].text = "Coins: " + playerCoins[1];
    }
}
