using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject hitEffect;
    public GameObject grave;

    public float health;
    public float damage;
    public float knockbackForce;

    public GameObject[] pickup;
    public float[] chance;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(9, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector2 direction = collision.gameObject.transform.position - transform.position;
            Instantiate(hitEffect, collision.GetContact(0).point, Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * knockbackForce);
        }
    }

    public void DropItem(Vector3 position)
    {
        float randomValue = Random.Range(0f, 1f);
        float last = 0;
        for (int i = 0; i < pickup.Length; i++) {
            if (i == 0 && randomValue == 0)
            {
                Instantiate(pickup[i], position, Quaternion.Euler(0, 0, 0));
            }
            else if (randomValue <= last + chance[i] && randomValue > last)
            {
                Instantiate(pickup[i], position, Quaternion.Euler(0, 0, 0));
            }
            last = chance[i];
        }
    }
}
