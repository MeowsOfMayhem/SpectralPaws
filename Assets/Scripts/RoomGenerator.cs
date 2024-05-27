using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] drzwi;
    public GameObject[] sprawner;
    public GameObject[] lockeddoor;
    public string kod_pokoju;
    public int wrogowie;
    public int boss;
    private int spawn;

    void Start()
    {

        char[] chars = kod_pokoju.ToCharArray();
        wrogowie = 0;
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

        unlockDoor();

        foreach (GameObject element in sprawner)
        {
            spawn = random.Next(2);
            element.SetActive(spawn == 1);
            if (element.tag == "Enemy" && spawn == 1)
                wrogowie++;
        }

    }

    void lockDoor()
    {
        Debug.Log("Locking");
        if (wrogowie > 0)
            foreach (GameObject element in lockeddoor)
            {
                // Debug.Log(chars[i]);
                //element.SetActive(random.Next(2) == 1);
                element.SetActive(true);
            }
    }

    public void unlockDoor()
    {
        Debug.Log("Unlocking");
        if (wrogowie == 0)
            foreach (GameObject element in lockeddoor)
            {
                // Debug.Log(chars[i]);
                //element.SetActive(random.Next(2) == 1);
                element.SetActive(false);
            }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            // Increase score by 1
            print("Wszedl");

            PlayerMovement2.instance.currentRoom = GetComponent<RoomGenerator>();
            Invoke("lockDoor", 1.5f);
        }
    }
    /*private bool NextBool(this System.Random r, int truePercentage = 50)
    {
        return r.NextDouble() < truePercentage / 100.0;
    }
    */
}
