using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update

    public Image serce;

    void Start()
    {
        serce.fillAmount = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        serce.fillAmount = 0.1f * PlayerMovement2.instance.playerHealth;
        if (PlayerMovement2.instance.playerHealth <= 0)
        {
            Debug.Log("Died !");
            PlayerMovement2.instance.EndGame();
        }
    }
}
