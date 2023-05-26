using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Population : MonoBehaviour
{
    public int maxPopulation = 50;
    public int[] curPopulation;

    [SerializeField] private TextMeshProUGUI[] populationText;

    private void Update()
    {
        populationText[0].text = (maxPopulation - curPopulation[0]).ToString();
        populationText[1].text = (maxPopulation - curPopulation[1]).ToString();
    }
}