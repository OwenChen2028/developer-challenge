using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject spawnEffect;

    public GameObject[] enemy;
    public float[] spawnForce;

    private List<Vector3> spawnPoints = new List<Vector3>();

    public bool bossReady;
    public bool levelDone;

    // Start is called before the first frame update
    void Start()
    {
        Tilemap spawnTilemap = transform.GetChild(0).GetComponent<Tilemap>();

        BoundsInt spawnBounds = spawnTilemap.cellBounds;
        TileBase[] spawnTiles = spawnTilemap.GetTilesBlock(spawnBounds);

        for (int x = 0; x < spawnBounds.size.x; x++)
        {
            for (int y = 0; y < spawnBounds.size.y; y++)
            {
                TileBase tile = spawnTiles[x + y * spawnBounds.size.x];
                if (tile != null)
                {
                    spawnPoints.Add(new Vector3(x + spawnBounds.x + 0.5f, y + spawnBounds.y + 0.5f, 0));
                }
            }
        }
        
        InvokeRepeating("SignalOne", 2.5f, 5f);
        InvokeRepeating("SpawnOne", 3f, 5f);
        InvokeRepeating("SignalTwo", 23.5f, 10f);
        InvokeRepeating("SpawnTwo", 24f, 10f);
        InvokeRepeating("SignalThree", 58.5f, 10f);
        InvokeRepeating("SpawnThree", 59f, 10f);
        Invoke("StopInvoke", 69.5f);
        Invoke("ReadyBoss", 70);
    }

    // Update is called once per frame
    void Update()
    {
        if (bossReady && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            Invoke("SignalFour", 0.5f);
            Invoke("SignalFour", 1.25f);
            Invoke("SignalFour", 2f);
            Invoke("SpawnFour", 2.75f);
            Invoke("EndLevel", 3f);

            bossReady = false;
        }
    }

    void SignalOne()
    {
        foreach (Vector3 point in spawnPoints)
        {
            Vector3 direction;
            if (Mathf.Abs(point.x) > Mathf.Abs(point.y))
            {
                direction = new Vector3(-1 * point.x, 0, 0);
            }
            else
            {
                direction = new Vector3(0, -1 * point.y, 0);
            }
            Instantiate(spawnEffect, point + direction.normalized, Quaternion.Euler(0, 0, 0));
        }
    }

    void SpawnOne()
    {
        foreach (Vector3 point in spawnPoints)
        {
            Vector3 direction;
            if (Mathf.Abs(point.x) > Mathf.Abs(point.y))
            {
                direction = new Vector3(-1 * point.x, 0, 0);
            }
            else
            {
                direction = new Vector3(0, -1 * point.y, 0);
            }
            GameObject instance = Instantiate(enemy[0], point, Quaternion.Euler(0, 0, 0));
            instance.GetComponent<Rigidbody2D>().AddForce(direction.normalized * spawnForce[0]);
        }
    }

    void SignalTwo()
    {
        foreach (Vector3 point in spawnPoints)
        {
            if (Mathf.Abs(point.x) > Mathf.Abs(point.y))
            {
                Vector3 direction;
                direction = new Vector3(-1 * point.x, 0, 0);
                Instantiate(spawnEffect, point + direction.normalized, Quaternion.Euler(0, 0, 0));
            }
        }
    }

    void SpawnTwo()
    {
        foreach (Vector3 point in spawnPoints)
        {
            if (Mathf.Abs(point.x) > Mathf.Abs(point.y))
            {
                Vector3 direction;
                direction = new Vector3(-1 * point.x, 0, 0);
                GameObject instance = Instantiate(enemy[1], point, Quaternion.Euler(0, 0, 0));
                instance.GetComponent<Rigidbody2D>().AddForce(direction.normalized * spawnForce[1]);
            }
        }
    }

    void SignalThree()
    {
        foreach (Vector3 point in spawnPoints)
        {
            if (Mathf.Abs(point.y) > Mathf.Abs(point.x))
            {
                Vector3 direction;
                direction = new Vector3(0, -1 * point.y, 0);
                Instantiate(spawnEffect, point + direction.normalized, Quaternion.Euler(0, 0, 0));
            }
        }
    }

    void SpawnThree()
    {
        foreach (Vector3 point in spawnPoints)
        {
            if (Mathf.Abs(point.x) < Mathf.Abs(point.y))
            {
                Vector3 direction;
                direction = new Vector3(0, -1 * point.y, 0);
                GameObject instance = Instantiate(enemy[1], point, Quaternion.Euler(0, 0, 0));
                instance.GetComponent<Rigidbody2D>().AddForce(direction.normalized * spawnForce[1]);
            }
        }
    }

    void StopInvoke()
    {
        CancelInvoke("SignalOne");
        CancelInvoke("SpawnOne");
        CancelInvoke("SignalTwo");
        CancelInvoke("SpawnTwo");
        CancelInvoke("SignalThree");
        CancelInvoke("SpawnThree");
    }

    void SignalFour()
    {
        Instantiate(spawnEffect, new Vector3(0.5f, 0.5f, 0f), Quaternion.Euler(0, 0, 0));
        Instantiate(spawnEffect, new Vector3(0.5f, -0.5f, 0f), Quaternion.Euler(0, 0, 0));
        Instantiate(spawnEffect, new Vector3(-0.5f, 0.5f, 0f), Quaternion.Euler(0, 0, 0));
        Instantiate(spawnEffect, new Vector3(-0.5f, -0.5f, 0f), Quaternion.Euler(0, 0, 0));
    }

    void SpawnFour()
    {
        Instantiate(enemy[2], new Vector3(0f, 0, 0f), Quaternion.Euler(0, 0, 0));
    }

    void ReadyBoss()
    {
        bossReady = true;
    }

    void EndLevel()
    {
        levelDone = true;
    }
}
