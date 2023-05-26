using UnityEngine;
using TMPro;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI spawnTimerText;
    private float spawnTimer = .5f;
    [SerializeField] private NPCManager npcManager;

    private float nextRoundTimer = 5;

    [SerializeField] private Population population;
    [SerializeField] private Coins coins;
    [SerializeField] private Income income;

    IEnumerator spawnNpcs(int npcsToSpawn, int playerID)
    {
        npcManager.SpawnNPC(npcManager.selectedNPC[playerID], playerID);
        npcsToSpawn--;
        yield return new WaitForSeconds(.5f);
        if(npcsToSpawn > 0)
        {
            StartCoroutine(spawnNpcs(npcsToSpawn, playerID));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        nextRoundTimer -= Time.deltaTime;

        if(spawnTimer <= 0)
        {
            npcManager.SpawnNPC(npcManager.selectedNPC[0], 0);
            npcManager.SpawnNPC(npcManager.selectedNPC[1], 1);
            spawnTimer = .5f;
        }

        if (nextRoundTimer <= 0)
        {
            coins.playerCoins[0] += income.playerIncome[0];
            coins.playerCoins[1] += income.playerIncome[1];

            population.curPopulation[0] = 0;
            population.curPopulation[1] = 0;

            nextRoundTimer = 30;
        }

        spawnTimerText.text = "<color=#f8ff6e>" + Mathf.Round(nextRoundTimer) + "</color>";
    }
}
