using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;



public class SleepSystem : MonoBehaviour
{
    public Service service;
    public FpsPlayerController playerController;
    [Header("Interaction")]
    public bool canInteract = true;
    public breads Breads;
    [SerializeField] private GameObject blackPanel;
    [HideInInspector]public TimeManager timeManager;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI subText;
    private string holder;
    private float writeSpeed = 0.1f;
    InputActionReference interactAction;
    void Update()
    {
        if (canInteract == true)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit; 

            if (Physics.Raycast(ray, out hit, 5f))
            {
                if(hit.collider.CompareTag("bed"))
                {
                    if (playerController.interactAction.action.WasPressedThisFrame())
                    {
                        Debug.Log("Sleeping...");
                        StartCoroutine(Sleep());
                    }
                }

            }
            
                
        }
    }

    IEnumerator Sleep()
    {
        blackPanel.SetActive(true);
        canInteract = false;
        playerController.enabled = false;

        yield return new WaitForSeconds(1f);
        if(service != null && service.Served)
        {
            TimeManager.Day++;
            if (TimeManager.Day > 6)
            {
                Debug.Log("Game Over");
            }
            TimeManager.Hour = 13;
            TimeManager.Minute = 00;
            playerController.health = playerController.health - 10;
            playerController.healthBar.value = playerController.health;
            playerController.stamina = playerController.stamina - 33;
            playerController.staminaBar.value = playerController.stamina;
            service.ResetForNewDay();
            

            holder = "DAY " + TimeManager.Day;
            foreach (char c in holder)
            {
                subText.text += c;
                yield return new WaitForSeconds(writeSpeed);

            }
        }
        else
        {
            TimeManager.Day = 1;
            TimeManager.Hour = 17;
            TimeManager.Minute = 50;
            Debug.Log("Anim + Game Over at 18.");
            holder = "DAY " + TimeManager.Day;
            foreach (char c in holder)
            {
                subText.text += c;
                yield return new WaitForSeconds(writeSpeed);

            }
        }

        yield return new WaitForSeconds(2f);
        subText.text = "";
        blackPanel.SetActive(false);
        canInteract = true;
        playerController.enabled = true;


    }
}


