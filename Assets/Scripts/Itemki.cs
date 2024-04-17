using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemki : MonoBehaviour
{
    [SerializeField]
    public RodzajItemu typ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            // Increase score by 1
            print("Hit!");

            if(typ == RodzajItemu.Zycie)
                if (PlayerMovement2.instance.playerHealth < 10)
                         PlayerMovement2.instance.playerHealth += 1;

            if (typ == RodzajItemu.Skrzynia)
                Debug.Log("Skrzynia");

            if (typ == RodzajItemu.Miecz)
                Debug.Log("Miecz");

            SFX.instance.PlaySFX(3);

            if (transform.parent != null)
                Destroy(transform.parent.gameObject);
            if (this != null)
                Destroy(this.gameObject);
        }
    }
}

public enum RodzajItemu
{
    Zycie,
    Skrzynia,
    Miecz,
    Jedzenie
}
