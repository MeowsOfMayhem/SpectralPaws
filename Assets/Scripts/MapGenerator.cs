using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    public float room_offset;
    public int map_size;
    public GameObject[] elements;

    private float xoffset=0, zoffset = 0;

    void Start()
    {
       // int[,] numbers = { { 1, 4, 2 }, { 3, 6, 8 } };
        GameObject[,] mapa = new GameObject[map_size, map_size];

        int startx = 3, startz = 3;


        for (int i = startx; i < mapa.GetLength(0); i++)
        {
            for (int j = startz; j < mapa.GetLength(1); j++)
            {
                // Instantiate(prefab, new Vector3(i * 2.0f, 0, 0), Quaternion.identity);
                GameObject impactDO = Instantiate(elements[0], new Vector3(xoffset,0, zoffset), Quaternion.identity);
                zoffset = zoffset + room_offset;
                Debug.Log(zoffset);
            }
            xoffset = xoffset + room_offset;
            Debug.Log(xoffset);
        }
    }

}
