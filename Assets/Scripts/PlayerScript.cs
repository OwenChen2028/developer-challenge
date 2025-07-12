using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Animator animator;
    private Vector3 scale;

    public GameObject grave;

    private Rigidbody2D body;
    public float moveForce;
    public float knockbackForce;

    public float health;
    public float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        scale = transform.localScale;

        body = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(8, 8);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - objectPos.x;

        if (mousePos.x >= 0)
        {
            transform.localScale = new Vector3(1 * scale.x, 1 * scale.y, 1 * scale.z);
        }
        else
        {
            transform.localScale = new Vector3(-1 * scale.x, 1 * scale.y, 1 * scale.z);
        }

        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }
    }

    void FixedUpdate()
    {
        body.AddForce(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * moveForce);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health -= collision.gameObject.GetComponent<EnemyScript>().damage;
            if (health <= 0)
            {
                Instantiate(grave, transform.position, Quaternion.Euler(0, 0, 0));
                Destroy(gameObject);
            }
            else
            {
                Vector2 direction = collision.gameObject.transform.position - transform.position;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * knockbackForce);
            }
        }
    }
}
