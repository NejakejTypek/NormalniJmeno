using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float curHealth = 1000;
    [SerializeField] private Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = curHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = curHealth;
    }

    public void TakeDamage(float damage)
    {
        curHealth -= damage;

        if (curHealth <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
