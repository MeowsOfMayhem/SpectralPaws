using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kula : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5.0f;
    private Vector3 whereistarget;
    public GameObject bulletsplash;
    private float lifeTime = 3.0f;
    public Rodzaj rodzaj;


// Start is called before the first frame update
    private void Start()
    {
        whereistarget = (PlayerMovement2.instance.transform.position - transform.position) * speed;
        lifeTime = Time.time + lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lifeTime)
        {
            if(transform.parent != null)
                Destroy(transform.parent.gameObject);
        }
        BulletMovement();
    }

    private void BulletMovement()
    {
        //Vector3 bulletVelocity =  // Vector3.up * speed;
        if(rodzaj == Rodzaj.Naprowadzany)
            whereistarget = (PlayerMovement2.instance.transform.position - transform.position) * speed;
        if (rodzaj == Rodzaj.Rakieta)
        {
            transform.LookAt(PlayerMovement2.instance.transform.position * speed);
            whereistarget = (PlayerMovement2.instance.transform.position - transform.position) * speed;
        }
        
            transform.Translate(whereistarget * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            // Increase score by 1
            print("Hit!");
            PlayerMovement2.instance.playerHealth -= 1;

            GameObject impactDO = Instantiate(bulletsplash, transform.position, Quaternion.identity);

            Destroy(impactDO, 2f);

            if (transform.parent != null)
                Destroy(transform.parent.gameObject);
            if (this != null)
                Destroy(this.gameObject);
        }

    }
}

public enum Rodzaj
{
    Zwykly,
    Naprowadzany,
    Granat,
    Rakieta
}
