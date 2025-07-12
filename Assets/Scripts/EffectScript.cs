using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{
    public float lifespan;
    public float variance;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartLifespan());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator StartLifespan()
    {
        yield return new WaitForSeconds(lifespan + Random.Range(-1 * variance, 1 * variance));
        Destroy(gameObject);
    }
}
