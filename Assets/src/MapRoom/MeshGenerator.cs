using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    MeshFilter meshFilter;
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        Debug.Log(meshFilter.mesh.vertices);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
