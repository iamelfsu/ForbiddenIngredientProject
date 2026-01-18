using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Rendering;
using JetBrains.Annotations;

public class CookTimer : MonoBehaviour
{
    public float currentTime;
    public float startingTime = 25f;
    public Pan pan;
    public Slider cookingBar;
    public bool finishedCooking;

    public TextMeshProUGUI countdownText;

    void Start()
    {
        currentTime = startingTime;
    }
    void Update()
    {

        currentTime -= 1 * Time.deltaTime;
        cookingBar.value += 1 * Time.deltaTime; 
        if (currentTime <= 0 && cookingBar.value >= 25)
        {
            cookingBar.value = 0;
            currentTime = 0;
            countdownText.text = "Cooked!";
            pan.CookedM();
            return;
        }
        

    }
    
}
