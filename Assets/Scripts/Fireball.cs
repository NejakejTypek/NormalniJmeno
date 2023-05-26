using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    int killedEnemies = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SimpleAI ai;
        collision.TryGetComponent<SimpleAI>(out ai);

        if(ai && killedEnemies == 0)
        {
            ai.TakeDamage(9999999);
            killedEnemies += 1;
            Destroy(gameObject);
        }
    }
}
