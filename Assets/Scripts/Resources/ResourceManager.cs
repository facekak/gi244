using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] woodTreePrefab;

    [SerializeField]
    private Transform woodTreeParent;

    [SerializeField]
    private ResourceSource[] resources;
    public ResourceSource[] Resource { get { return resources; } }

    public static ResourceManager instance;

    void Awake()
    {
        instance = this;
    }


    // Update is called once per frame
    void Start()
    {
        FindAllResource();
    }
    
    private void FindAllResource()
    {
        resources = FindObjectsOfType<ResourceSource>();
    }
    
    
}

