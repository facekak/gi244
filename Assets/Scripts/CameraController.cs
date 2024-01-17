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

    [Header("Zoom")] 
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float zoomModifier;
    
    [SerializeField] private float minZoomDist;
    [SerializeField] private float maxZoomDist;

    [SerializeField] private float dist;

    [Header("Rotate")] 
    [SerializeField] private float rotationAmount;
    [SerializeField] private Quaternion newRotation;
    
    public static CameraController instance;

    private void Awake()
    {
        instance = this;
        cam = Camera.main;

        newRotation = transform.rotation;
        rotationAmount = 1;
    }

    void Start()
    {
        moveSpeed = 50;

        zoomSpeed = 25;
        minZoomDist = 15;
        maxZoomDist = 50;
    }

    // Update is called once per frame
    void Update()
    {
        moveByKB();
        Zoom();
        Rotate();
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

    private void Zoom()
    {
        zoomModifier = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetKey(KeyCode.Z))
            zoomModifier = 0.01f;
        
        if (Input.GetKey(KeyCode.X))
            zoomModifier = -0.01f;

        dist = Vector3.Distance(transform.position, cam.transform.position);

        if (dist < minZoomDist && zoomModifier > 0f)
            return; // too close
        else if (dist > minZoomDist && zoomModifier < 0f)
            return; // too far
        
        cam.transform.position += cam.transform.forward * zoomModifier * zoomSpeed;
        
    }
    
    void Rotate()
    {
        if (Input.GetKey(KeyCode.Q))
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);

        if (Input.GetKey(KeyCode.E))
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * moveSpeed);
    }
}
