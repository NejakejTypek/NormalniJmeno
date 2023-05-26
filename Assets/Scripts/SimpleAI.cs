using UnityEngine;
using UnityEngine.UI;

public class SimpleAI : MonoBehaviour
{
    public Vector2 destination;
    public GameObject destinationObject;
    public float moveSpeed;
    public GameObject graphic;

    public GameObject attackAnim;
    public GameObject moveAnim;

    public int playerID;

    public Transform npcsFolder;

    public GameObject enemyPlayerCrystal;

    public float stoppingDistance;

    public Population population;

    public float damage;

    public float curHealth;
    public float attackSpeed;

    public Slider healthBar;
    public float maxHealth;

    public float attackTimer;

    public GameObject teamCircle;

    private void Start()
    {
        healthBar.maxValue = maxHealth;

        if (playerID == 0)
            teamCircle.GetComponent<Image>().color = Color.blue;
        else
            teamCircle.GetComponent<Image>().color = Color.red;
    }

    private void Update()
    {
        healthBar.value = curHealth;

        FindDestination();

        if (destinationObject == null)
            return;

        if (Vector2.Distance(transform.position,destination) <= stoppingDistance)
        {
            //Attacking
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            moveAnim.SetActive(false);
            attackAnim.SetActive(true);

            SimpleAI enemy;
            bool isEnemy = destinationObject.TryGetComponent(out enemy);

            if(isEnemy)
            {
                attackTimer -= Time.deltaTime;

                if(attackTimer <= 0)
                {
                    enemy.TakeDamage(damage);
                    attackTimer = attackSpeed;
                } 
            }
            else
            {
                attackTimer -= Time.deltaTime;

                if (attackTimer <= 0)
                {
                    destinationObject.GetComponent<Crystal>().health.TakeDamage(damage);
                    attackTimer = .75f;
                }
            }
        }
        else
        {
            //Moving
            moveAnim.SetActive(true);
            attackAnim.SetActive(false);

            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            if (destination.x < transform.position.x)
            {
                if(GetComponent<Rigidbody2D>().velocity.x > -moveSpeed)
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-moveSpeed * Time.deltaTime, 0), ForceMode2D.Impulse);

                graphic.transform.eulerAngles = new Vector3(0, 180, 0);
            } 
            else
            {
                if (GetComponent<Rigidbody2D>().velocity.x < moveSpeed)
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(moveSpeed * Time.deltaTime, 0), ForceMode2D.Impulse);

                graphic.transform.eulerAngles = new Vector3(0, 0, 0);
            }

            if (Mathf.Abs(transform.position.x - destinationObject.transform.position.x) <= stoppingDistance + 3)
            {
                if (destination.y < transform.position.y)
                    if (GetComponent<Rigidbody2D>().velocity.y > -moveSpeed)
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(0,- moveSpeed * Time.deltaTime), ForceMode2D.Impulse);
                else
                    if (GetComponent<Rigidbody2D>().velocity.y < moveSpeed)
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, moveSpeed * Time.deltaTime), ForceMode2D.Impulse);
            }
        }
    }

    private void FindDestination()
    {
        Vector2 nearestDestination = new Vector2(9999, 9999);
        float nearestDestinationDistance = 99999;
        GameObject nearestDestinationObject = null;

        for(int i=0; i < npcsFolder.transform.childCount; i++)
        {
            if(npcsFolder.GetChild(i).GetComponent<SimpleAI>().playerID != playerID)
            {
                if(nearestDestination == new Vector2(9999, 9999))
                {
                        nearestDestination = npcsFolder.GetChild(i).position;
                        nearestDestinationDistance = Vector2.Distance(npcsFolder.GetChild(i).transform.position, transform.position);
                        nearestDestinationObject = npcsFolder.GetChild(i).gameObject;
                }
                else
                {
                    //Checks if AI is nearer than nearest found AI b4
                    if(nearestDestinationDistance > Vector2.Distance(transform.position, npcsFolder.GetChild(i).transform.position))
                    {
                            nearestDestination = npcsFolder.GetChild(i).transform.position;
                            nearestDestinationDistance = Vector2.Distance(transform.position, npcsFolder.GetChild(i).transform.position);
                            nearestDestinationObject = npcsFolder.GetChild(i).gameObject;
                    }
                }
            }
        }

        if(nearestDestinationDistance == 99999)
        {
            nearestDestination = enemyPlayerCrystal.transform.position;
            nearestDestinationObject = enemyPlayerCrystal;
        }

        destination = nearestDestination;
        destinationObject = nearestDestinationObject;
    }

    public void TakeDamage(float damage)
    {
        curHealth -= damage;

        if(curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
