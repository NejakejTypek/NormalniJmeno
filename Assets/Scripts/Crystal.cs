using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public Health health;
    [SerializeField] private Transform npcsFolder;
    public GameObject fireBallPrefab;
    float attackTimer;

    private void Update()
    {
        attackTimer -= Time.deltaTime;

        if(attackTimer <= 0)
        {
            GameObject nearestEnemy = null;

            for (int i = 0; i < npcsFolder.childCount; i++)
            {
                if (npcsFolder.GetChild(i).GetComponent<SimpleAI>().enemyPlayerCrystal == gameObject)
                {
                    if(nearestEnemy == null)
                    {
                        nearestEnemy = npcsFolder.GetChild(i).gameObject;
                    }
                    else
                    {
                        if(Vector2.Distance(transform.position, nearestEnemy.transform.position) > Vector2.Distance(transform.position, npcsFolder.GetChild(i).transform.position))
                        {
                            nearestEnemy = npcsFolder.GetChild(i).gameObject;
                        }
                    }
                }
            }

            if(nearestEnemy == null)
            {
                attackTimer = 1;
                return;
            }

            if(Vector2.Distance(transform.position, nearestEnemy.transform.position) <= 5)
            {
                GameObject fireBallClone = Instantiate(fireBallPrefab);
                fireBallClone.transform.position = transform.position;

                if(nearestEnemy.GetComponent<SimpleAI>().playerID == 0)
                    fireBallClone.GetComponent<Rigidbody2D>().AddForce(new Vector2(-25, 0), ForceMode2D.Impulse);
                else
                    fireBallClone.GetComponent<Rigidbody2D>().AddForce(new Vector2(25, 0), ForceMode2D.Impulse);
            }

            attackTimer = 1;
        }

        
    }
}
