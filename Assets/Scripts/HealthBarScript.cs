using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    private Vector3 scale;

    public GameObject player;
    public float trackSize;
    private bool handleActive;

    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject handle = transform.GetChild(1).gameObject;
        float health = player.GetComponent<PlayerScript>().health;
        float maxHealth = player.GetComponent<PlayerScript>().maxHealth;

        if (health < maxHealth)
        {
            if (!handle.activeSelf)
            {
                handle.SetActive(true);
            }
            handle.transform.localPosition = new Vector3(-1 * (trackSize / 2) + (health / maxHealth) * trackSize, 0, 0);
        }
        else
        {
            if (handle.activeSelf)
            {
                handle.SetActive(false);
            }
        }

        
    }

    void LateUpdate()
    {
        Vector3 parentScale = player.transform.localScale;
        transform.localScale = new Vector3(Mathf.Sign(parentScale.x) * scale.x, scale.y, scale.z);
    }
}
