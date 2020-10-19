using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    // Start is called before the first frame update
    public static Tiles Instance { get; private set; }
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
 
    public List<GameObject> tiles;
    public GameObject[] tilesArray; 
      void Awake()
    {
        Instance = this; 
        tilesArray = tiles.ToArray();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
