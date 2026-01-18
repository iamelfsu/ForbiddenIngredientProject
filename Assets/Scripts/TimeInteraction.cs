using UnityEngine;
using System.Collections;


public class TimeInteraction : MonoBehaviour
{
    [HideInInspector] public TimeManager timeManager;
    public GameObject hand;
    public GameObject body;
    public GameObject leg;
    public GameObject head;
    public Service service;
    public Transform SpawnPointI;
    public FpsPlayerController playerController;

    private void OnEnable()
    {
        TimeManager.OnMinuteChanged += timeCheck;
        TimeManager.OnHourChanged += timeCheck18;
        TimeManager.OnDayChanged += timeCheckNight;  
    }
    private void OnDisable()
    {
        TimeManager.OnMinuteChanged -= timeCheck;
        TimeManager.OnHourChanged -= timeCheck18;
        TimeManager.OnDayChanged -= timeCheckNight;
    }
    void timeCheck18()
    {
        if (TimeManager.Hour == 17 && TimeManager.Minute == 50)
        {
            if (service == null && service.Served == false)
            {
                Debug.Log("GameOver Animation +5  and UI at 18");
                
                return;
            }
            if (service != null && service.Served)
            {
                service.ConsumeAt18();
            }          
        }
    }


    void timeCheckNight()
    {
        if(TimeManager.Hour == 00)
            playerController.health = playerController.health - 10;
            playerController.healthBar.value = playerController.health;
            playerController.stamina = playerController.stamina - 33;
            playerController.staminaBar.value = playerController.stamina;
    }
   
    void timeCheck()
    {
        
        if (TimeManager.Hour == 14 && TimeManager.Minute == 00 && TimeManager.Day == 1)
        {
            SpawnObject(hand);
            playerController.health = playerController.health - 10;
            playerController.healthBar.value = playerController.health;
        }
        if (TimeManager.Hour == 14 && TimeManager.Minute == 00 && TimeManager.Day == 2)
        {
            SpawnObject(hand);
            playerController.health = playerController.health - 10;
            playerController.healthBar.value = playerController.health;
        }
        if (TimeManager.Hour == 14 && TimeManager.Minute == 00 && TimeManager.Day == 3)
        {
            SpawnObject(leg);
            playerController.health = playerController.health - 10;
            playerController.healthBar.value = playerController.health;
        }
        if (TimeManager.Hour == 14 && TimeManager.Minute == 00 && TimeManager.Day == 4)
        {
            SpawnObject(leg);
            playerController.health = playerController.health - 10;
            playerController.healthBar.value = playerController.health;

        }
        if (TimeManager.Hour == 14 && TimeManager.Minute == 00 && TimeManager.Day == 5)
        {
            SpawnObject(head);
            playerController.health = playerController.health - 10;
            playerController.healthBar.value = playerController.health;
       
        }
        if (TimeManager.Hour == 14 && TimeManager.Minute == 00 && TimeManager.Day == 6)
        {
            SpawnObject(body);
            playerController.health = playerController.health - 10;
            playerController.healthBar.value = playerController.health;
  
        }
    }

    private void SpawnObject(GameObject prefab)
    {
        Instantiate(prefab, SpawnPointI.transform.position, Quaternion.identity);
    }
}
