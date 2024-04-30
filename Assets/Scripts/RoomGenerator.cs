using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] drzwi;
    public GameObject[] sprawner;
    public string kod_pokoju;

    void Start()
    {
        var random = new System.Random();
        foreach (GameObject element in drzwi)
        {
            element.SetActive(random.Next(2) == 1);
            kod_pokoju += element.activeSelf.ToString();
        }

        foreach (GameObject element in sprawner)
        {
            element.SetActive(random.Next(2) == 1);
        }
    }

    /*private bool NextBool(this System.Random r, int truePercentage = 50)
    {
        return r.NextDouble() < truePercentage / 100.0;
    }
    */
}
