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
        coinsText[0].text = playerCoins[0].ToString();
        coinsText[1].text = playerCoins[1].ToString();
    }
}
