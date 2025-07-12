using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunScript : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator animator;
    private Vector3 scale;
    private int order;

    public GameObject player;

    public GameObject fireEffect;
    public GameObject bullet;

    private bool isFiring;
    private bool onCooldown;
    public float fireVelocity;
    public float fireCooldown;
    public float recoilForce;

    private List<Coroutine> gunFire = new List<Coroutine>();

    // Start is called before the first frame update
    void Start()
    {
        sprite = transform.GetChild(2).GetComponent<SpriteRenderer>();
        animator = transform.GetChild(2).GetComponent<Animator>();
        scale = transform.localScale;
        order = sprite.sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (mousePos.x >= 0)
        {
            transform.localScale = new Vector3(1 * scale.x, 1 * scale.y, 1 * scale.z);
        }
        else
        {
            transform.localScale = new Vector3(-1 * scale.x, -1 * scale.y, 1 * scale.z);
        }

        if (angle <= 0 || angle >= 180)
        {
            sprite.sortingOrder = order;
        }
        else
        {
            sprite.sortingOrder = -1 * order;
        }

        if (Input.GetButton("Fire1") && !isFiring && !onCooldown)
        {
            gunFire.Add(StartCoroutine(FireGun()));
        }
    }

    void FixedUpdate()
    {
        if (isFiring)
        {
            animator.SetTrigger("Fire");

            Instantiate(fireEffect, transform.GetChild(1), false); //Instantiate(fireEffect, transform.GetChild(1).position, transform.rotation);

            GameObject instance = Instantiate(bullet, transform.GetChild(0).position, transform.GetChild(0).rotation);
            instance.GetComponent<Rigidbody2D>().velocity = instance.transform.right * fireVelocity;

            player.GetComponent<Rigidbody2D>().AddForce(-1 * instance.transform.right * recoilForce);

            isFiring = false;
        }
    }

    public IEnumerator FireGun()
    {
        isFiring = true;
        onCooldown = true;
        yield return new WaitForSeconds(fireCooldown);
        onCooldown = false;
    }

    public void ReloadGun()
    {
        foreach (Coroutine instance in gunFire)
        {
            StopCoroutine(instance);
        }
        onCooldown = false;
    }
}
