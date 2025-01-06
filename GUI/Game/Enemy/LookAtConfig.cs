﻿using System;
using UnityEngine;

public class LookAtConfig : MonoBehaviour
{
    private void Update()
    {
        var rotation = Camera.main.transform.rotation;
        transform.LookAt(
            transform.position + rotation * Vector3.forward, 
            rotation * Vector3.up
        );
    }
}