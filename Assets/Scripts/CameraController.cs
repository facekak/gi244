using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [Header("Move")] 
    [SerializeField] private float moveSpeed;

    [SerializeField] private Transform conner1;
    [SerializeField] private Transform conner2;

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
        transform.position = Clamp(conner1.position, conner2.position);
    }

    private Vector3 Clamp(Vector3 lowerleft,Vector3 topRight)
    {
        Vector3 pos = new Vector3(Mathf.Clamp(transform.position.x, lowerleft.x, topRight.y), transform.position.y,
            Mathf.Clamp(transform.position.z, lowerleft.z, topRight.z));

        return pos;
    }
}
