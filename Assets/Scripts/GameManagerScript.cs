using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject player;
    public GameObject spawner;
    public float exitTime;

    private bool firerateBoosted;
    private List<Coroutine> firerateBoost = new List<Coroutine>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null || (spawner.GetComponent<EnemySpawnerScript>().levelDone && GameObject.FindGameObjectsWithTag("Enemy").Length == 0))
        {
            StartCoroutine(StartExit());
        }
    }

    public IEnumerator StartExit()
    {
        yield return new WaitForSeconds(exitTime);
        SceneManager.LoadScene("Start");
    }

    public void StartFirerateBoost(float duration, float amount)
    {
        if (firerateBoosted)
        {
            firerateBoosted = false;
            foreach (Coroutine instance in firerateBoost)
            {
                StopCoroutine(instance);
            }
            player.transform.GetChild(0).GetChild(1).gameObject.GetComponent<PlayerGunScript>().fireCooldown *= amount;
        }
        firerateBoost.Add(StartCoroutine(BoostFirerate(duration, amount)));
    }

    public IEnumerator BoostFirerate(float duration, float amount)
    {
        firerateBoosted = true;
        player.transform.GetChild(0).GetChild(1).gameObject.GetComponent<PlayerGunScript>().fireCooldown /= amount;
        player.transform.GetChild(0).GetChild(1).gameObject.GetComponent<PlayerGunScript>().ReloadGun();
        yield return new WaitForSeconds(duration);
        firerateBoosted = false;
        if (player != null)
        {
            player.transform.GetChild(0).GetChild(1).gameObject.GetComponent<PlayerGunScript>().fireCooldown *= amount;
        }
    }
}
