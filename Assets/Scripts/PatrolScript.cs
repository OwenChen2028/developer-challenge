using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolScript : MonoBehaviour
{
    private Rigidbody2D body;
    private Transform player;
    public float moveForce;
    public float turnSpeed;

    public string mode;
    public float chaseTime;
    public float retreatTime;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        StartCoroutine(StartChase());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 direction = new Vector3(0, 0, 0);
            if (mode == "chase")
            {
                direction = player.position - transform.position;
            }
            else if (mode == "retreat")
            {
                direction = -1 * transform.position;
            }
            transform.up = Vector3.Lerp(transform.up, direction.normalized, turnSpeed);
            body.AddForce(transform.up * moveForce);
        }
    }

    public IEnumerator StartChase()
    {
        mode = "chase";
        yield return new WaitForSeconds(chaseTime);
        mode = "retreat";
        StartCoroutine(StartRetreat());
    }

    public IEnumerator StartRetreat()
    {
        mode = "retreat";
        yield return new WaitForSeconds(retreatTime);
        mode = "chase";
        StartCoroutine(StartChase());
    }
}
