using UnityEngine;
using TMPro;

public class Income : MonoBehaviour
{
    public int baseIncome;
    public int[] playerIncome;
    public TextMeshProUGUI[] incomeText;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i < playerIncome.Length; i++)
        {
            playerIncome[i] = baseIncome;
        }
    }

    private void Update()
    {
        incomeText[0].text = playerIncome[0].ToString();
        incomeText[1].text = playerIncome[1].ToString();
    }
}
