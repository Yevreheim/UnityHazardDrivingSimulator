﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class CameraMovement : MonoBehaviour
{
    //Variables
    private int speedGo;
    private float speedEnhancer = 3;

    // Start is called before the first frame update
    void Start()
    {
        speedGo = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
        //transform.eulerAngles = new Vector3(0,0,Input.GetAxis("Horizontal")*360);
        //Wheel
        
        if ((Input.GetKey(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.D)))
        {
            if (transform.position.x < 21.5){
                transform.Translate(new Vector3(Input.GetAxis("Horizontal"),0,0));
                transform.Translate(new Vector3(5 * Time.deltaTime, 0, 0));
            }
        }
        if ((Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.A)))
        {
            if (transform.position.x > -3.5){
                transform.Translate(new Vector3(Input.GetAxis("Horizontal"),0,0));
                transform.Translate(new Vector3(-5 * Time.deltaTime, 0, 0));
            }
        }
        if ((Input.GetKey(KeyCode.DownArrow)) || (Input.GetKey(KeyCode.S)))
        {
            if (speedEnhancer > 3){
                speedEnhancer = speedEnhancer - 0.1f;
            }
        }
        if ((Input.GetKey(KeyCode.UpArrow)) || (Input.GetKey(KeyCode.W)))
        {
            speedEnhancer = speedEnhancer + 0.1f;
        }
        if ((Input.GetKey(KeyCode.Space)))
        {
            switch (speedGo)
            {
                case 0:
                    Debug.Log("Accelerating");
                    Thread.Sleep(100);
                    speedGo = 1;
                    break;
                case 1:
                    Debug.Log("Breaking");
                    Thread.Sleep(100);
                    speedEnhancer = 3;
                    speedGo = 0;
                    break;
            }
        }
        switch (speedGo)
        {
            case 0:
                break;
            case 1:
                transform.Translate(new Vector3(0, 0, speedEnhancer * Time.deltaTime));
                break;
        }
        
    }
}
