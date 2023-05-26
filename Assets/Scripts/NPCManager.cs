using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class NPCManager : MonoBehaviour
{
    [System.Serializable]
    public class NPC
    {
        public string npcName;
        public Texture npcIcon;
        public GameObject npcPrefab;
        public string npcDescription;
        public int npcPrice;
        public int npcArmor;
        public float npcAttackSpeed;
        public float npcMoveSpeed;
        public float npcRange;
        public int npcSpace;
        public int maxHealth;
        public float npcBaseDamage;
        public enum ArmorType
        {
            Light,
            Medium,
            Heavy
        }
        public ArmorType armorType;
        [Header("Damage")]
        public int npcLightArmorMultiplier;
        public int npcMediumArmorMultiplier;
        public int npcHeavyArmorMultiplier;
    }
    public NPC[] npcs;

    [SerializeField] Transform[] playerSpawner;
    [SerializeField] private Coins coins;
    [SerializeField] private Population population;
    [SerializeField] private Income income;

    [SerializeField] Transform[] playerNPCButtonsParent;
    [SerializeField] private GameObject spawnNPCButtonPrefab;

    [SerializeField] private Transform npcsFolder;

    [SerializeField] private GameObject npcInfo;

    public int[] selectedNPC;

    void Start()
    {
        for(int i=0; i < npcs.Length; i++)
        {
            for(int j=0; j < playerNPCButtonsParent.Length; j++)
            {
                GameObject spawnNPCButtonClone = Instantiate(spawnNPCButtonPrefab);
                spawnNPCButtonClone.transform.Find("NPC_ICON").GetComponent<RawImage>().texture = npcs[i].npcIcon;
                spawnNPCButtonClone.transform.SetParent(playerNPCButtonsParent[j]);

                int k = i;
                int b = j;
                spawnNPCButtonClone.GetComponent<Button>().onClick.AddListener(() => selectedNPC[b] = k);

                EventTrigger trigger = spawnNPCButtonClone.GetComponent<EventTrigger>();
                EventTrigger.Entry entryEnter = new EventTrigger.Entry();
                entryEnter.eventID = EventTriggerType.PointerEnter;
                entryEnter.callback.AddListener((data) => { ShowNPCInfo((PointerEventData)data, k); });

                trigger.triggers.Add(entryEnter);

                EventTrigger.Entry entryExit = new EventTrigger.Entry();
                entryExit.eventID = EventTriggerType.PointerExit;
                entryExit.callback.AddListener((data) => { HideNPCInfo((PointerEventData)data, k); });

                trigger.triggers.Add(entryExit);
            }
        }
    }

    void Update()
    {
        if(npcInfo.activeSelf)
        {
            npcInfo.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y + 100);
        }
    }

    public void SpawnNPC(int npcID, int playerID)
    {
        if((coins.playerCoins[playerID] - npcs[npcID].npcPrice) >= 0 && population.curPopulation[playerID] + npcs[npcID].npcSpace <= population.maxPopulation)
        {
            coins.playerCoins[playerID] -= npcs[npcID].npcPrice;
            population.curPopulation[playerID] += npcs[npcID].npcSpace;
            income.playerIncome[playerID] += Mathf.RoundToInt(npcs[npcID].npcPrice * 0.2f);

            GameObject npcClone = Instantiate(npcs[npcID].npcPrefab);
            npcClone.transform.position = new Vector2(playerSpawner[playerID].position.x + Random.Range(-3f,3f), playerSpawner[playerID].position.y + Random.Range(-3f, 3f));

            SimpleAI simpleAI = npcClone.GetComponent<SimpleAI>();

            simpleAI.moveSpeed = npcs[npcID].npcMoveSpeed;
            simpleAI.npcsFolder = npcsFolder;
            simpleAI.playerID = playerID;
            simpleAI.maxHealth = npcs[npcID].maxHealth;
            simpleAI.curHealth = npcs[npcID].maxHealth;
            simpleAI.damage = npcs[npcID].npcBaseDamage;
            simpleAI.population = population;
            simpleAI.stoppingDistance = npcs[npcID].npcRange;
            simpleAI.attackSpeed = npcs[npcID].npcAttackSpeed;

            if(playerID == 0)
                simpleAI.enemyPlayerCrystal = playerSpawner[1].gameObject;
            else
                simpleAI.enemyPlayerCrystal = playerSpawner[0].gameObject;

            npcClone.transform.SetParent(npcsFolder);
        }
    }

    public void ShowNPCInfo(PointerEventData e,int npcID)
    {
        npcInfo.transform.Find("NPC Name").GetComponent<TextMeshProUGUI>().text = npcs[npcID].npcName;
        npcInfo.transform.Find("NPC Description").GetComponent<TextMeshProUGUI>().text = npcs[npcID].npcDescription;
        npcInfo.transform.Find("NPC Price").GetComponent<TextMeshProUGUI>().text = "Price: " + npcs[npcID].npcPrice;
        npcInfo.transform.Find("NPC Health").GetComponent<TextMeshProUGUI>().text = "Health: " + npcs[npcID].maxHealth;
        npcInfo.transform.Find("NPC Income").GetComponent<TextMeshProUGUI>().text = "Income for Recruit: " + Mathf.RoundToInt(npcs[npcID].npcPrice * .2f);
        npcInfo.SetActive(true);
    }

    public void HideNPCInfo(PointerEventData e, int npcID)
    {
        npcInfo.SetActive(false);
    }
}
