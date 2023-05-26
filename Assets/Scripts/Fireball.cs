using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SimpleAI ai;
        collision.TryGetComponent<SimpleAI>(out ai);

        if(ai)
        {
            ai.TakeDamage(9999999);
            Destroy(gameObject);
        }
    }
}
