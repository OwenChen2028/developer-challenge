﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortOrderScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100) * -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
