using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    private GameObject gameManager;
    public string id;
    public float duration;
    public float amount;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (id == "health")
            {
                float health = other.GetComponent<PlayerScript>().health;
                float maxHealth = other.GetComponent<PlayerScript>().maxHealth;

                other.GetComponent<PlayerScript>().health = Mathf.Min(health + amount, maxHealth);
                Destroy(gameObject);
            }
            else if (id == "firerate")
            {
                gameManager.GetComponent<GameManagerScript>().StartFirerateBoost(duration, amount);
                Destroy(gameObject);
            }
        }
    }
}
