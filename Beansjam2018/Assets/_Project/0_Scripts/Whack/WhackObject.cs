﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhackObject : MonoBehaviour {

    bool touched = false;
    float whackSpeed;
    float timer;
    float timerMax;
    Vector3 originalPosition;

    private void Start()
    {
        whackSpeed = -2.5f;
        timerMax = 0.5f;
        timer = 0;
        originalPosition = transform.position;
    }

    void Update()
    {
        if(touched && timer <= timerMax)
        {
            timer += Time.deltaTime;
            this.transform.Translate(new Vector3(0, whackSpeed * Time.deltaTime, 0));
        }
    }

    public void setTouched(bool touched)
    {
        this.touched = touched;
    }

    public bool getTouched()
    {
        return this.touched;
    }

    public void resetObject()
    {
        transform.position = originalPosition;
        this.setTouched(false);
        this.timer = 0;
    }
}
