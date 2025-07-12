using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseScript : MonoBehaviour
{
    private Animator animator;
    private Vector3 scale;

    private Rigidbody2D body;
    private Transform player;
    public float moveForce;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        scale = transform.localScale;

        body = GetComponent<Rigidbody2D>();
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {

            if (player.position.x - transform.position.x >= 0)
            {
                transform.localScale = new Vector3(1 * scale.x, 1 * scale.y, 1 * scale.z);
            }
            else
            {
                transform.localScale = new Vector3(-1 * scale.x, 1 * scale.y, 1 * scale.z);
            }

            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 direction = player.position - transform.position;
            body.AddForce(direction.normalized * moveForce);
        }
    }
}
