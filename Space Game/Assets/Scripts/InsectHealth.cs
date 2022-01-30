using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectHealth : MonoBehaviour
{
    [SerializeField] private int insectHealth = 1;

    private int currentHealth;

    private void OnEnable()
    {
        currentHealth = insectHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.SetActive(false);
            return;
        }
        else
        {
            currentHealth--;
        }
        
        if (currentHealth == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
