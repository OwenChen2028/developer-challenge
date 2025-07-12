using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    public GameObject hitEffect;

    public float lifespan;
    public bool destroyed;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartLifespan());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!destroyed) {
            if (other.tag == "Enemy")
            {
                Instantiate(hitEffect, other.ClosestPoint(transform.position), transform.rotation);
                Destroy(gameObject);
                destroyed = true;

                other.GetComponent<EnemyScript>().health -= damage;
                if (other.GetComponent<EnemyScript>().health <= 0)
                {
                    Instantiate(other.GetComponent<EnemyScript>().grave, other.transform.position, Quaternion.Euler(0, 0, 0));
                    other.GetComponent<EnemyScript>().DropItem(other.transform.position);
                    Destroy(other.gameObject);
                }
            }
            else if (other.tag == "Wall" || other.tag == "Bound")
            {
                Instantiate(hitEffect, other.ClosestPoint(transform.position), transform.rotation);
                Destroy(gameObject);
                destroyed = true;
            }
        }
    }

    public IEnumerator StartLifespan()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
        destroyed = true;
    }
}
