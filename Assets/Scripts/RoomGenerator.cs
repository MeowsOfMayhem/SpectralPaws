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

        char[] chars = kod_pokoju.ToCharArray();
        int i;
        i = 0;
        var random = new System.Random();
        //Debug.Log(kod_pokoju);
        foreach (GameObject element in drzwi)
        {
           // Debug.Log(chars[i]);
            //element.SetActive(random.Next(2) == 1);
            if (chars[i] == '1')
                element.SetActive(true);
            else
                element.SetActive(false);
            i++;
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
