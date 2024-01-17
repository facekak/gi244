using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [Header("Move")] 
    [SerializeField] private float moveSpeed;

    [SerializeField] private float xinput;
    [SerializeField] private float zinput;

    public static CameraController instance;

    private void Awake()
    {
        instance = this;
        cam = Camera.main;
    }

    void Start()
    {
        moveSpeed = 50;
    }

    // Update is called once per frame
    void Update()
    {
        moveByKB();
    }

    private void moveByKB()
    {
        xinput = Input.GetAxis("Horizontal");
        zinput = Input.GetAxis("Vertical");

        Vector3 dir = (transform.forward * zinput) + (transform.right * xinput);

        
        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}
