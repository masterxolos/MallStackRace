using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CashierCheckpoint : MonoBehaviour
{
    [SerializeField] private Canvas winCanvas;

    [SerializeField] private Canvas failCanvas;

    [SerializeField] private Slider playerSlider;
    [SerializeField] private Slider bot1Slider;
    [SerializeField] private Slider bot2Slider;
    [SerializeField] private Canvas playerCanvas;


    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (playerSlider.maxValue == playerSlider.value)
            {
                winCanvas.enabled = true;
                playerCanvas.enabled = false;
                Debug.Log("Burada çalıştı");
                
            }
        }

        else if (other.gameObject.CompareTag("Bot1"))
        {
            if (bot1Slider.maxValue == bot1Slider.value)
            {
                failCanvas.enabled = true;
            }
        }

        else if (other.gameObject.CompareTag("Bot2"))
        {
            if (bot2Slider.maxValue == bot2Slider.value)
            {
                failCanvas.enabled = true;
            }
        }
    }
}
