﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1 : Type1
{
    public GameObject ramp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ActivateRamp();
    }

    void ActivateRamp()
    {
        if(currentHealth <= 0)
        {
            ramp.SetActive(true);
        }
    }
}
