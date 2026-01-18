using System.Threading.Tasks;
using UnityEngine;

public class Service : MonoBehaviour
{
    public FpsPlayerController playerController;
    public GameObject burgerPrefab;
    public Transform burgerSpawnPoint;
    public CarryManager.HeldType requiredItem = CarryManager.HeldType.Burger;
    public bool Served;
    private GameObject spawnedBurger;
    private bool playerInRange;


    private void Start()
    {
        ResetForNewDay();
    }


    void Update()
    {
        if (!playerInRange)
        {
            return;
        }
        if(playerController.interactAction.action.WasPressedThisFrame())
        {
            doService();
        }
    }


    public void ResetForNewDay()
    {
        Served = false;
        if (spawnedBurger != null)
        {
            Destroy(spawnedBurger);
            spawnedBurger = null;
        }
    }

    private void doService()
    {
        
        
        if (Served)
        {
            return;
        }

        if (!CarryManager.HoldingItem)
        {
            return;
        }

        if (CarryManager.HeldItemType != requiredItem)
        {
            return;
        }

        if (CarryManager.HeldItem != null)
        {
            Destroy(CarryManager.HeldItem);
        }

        CarryManager.HoldingItem = false;
        CarryManager.HeldItemType = CarryManager.HeldType.None;
        CarryManager.HeldItem = null;
        spawnedBurger = Instantiate(burgerPrefab, burgerSpawnPoint.position, burgerSpawnPoint.rotation);

        Served = true;
        Debug.Log("Service completed: Burger served.");

    }

    public void ConsumeAt18()
    {
        if(spawnedBurger != null)
        {
            Destroy(spawnedBurger);
            Debug.Log("Burger consumed at 18:00.");
            spawnedBurger = null;
        }
    }

    private void OnEnable()
    {
        TimeManager.OnDayChanged += ResetForNewDay;
    }
    private void OnDisable()
    {
        TimeManager.OnDayChanged -= ResetForNewDay;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }  
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}




