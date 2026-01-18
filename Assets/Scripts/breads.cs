using UnityEngine;

public class breads : MonoBehaviour
{
    public FpsPlayerController playerController;
    public GameObject Burger;
    public Transform burgerSpawnPoint;
    private bool playerInRange;
    [SerializeField] private Material outlineMaterial;

    public CarryManager.HeldType[] allowedTypes = new CarryManager.HeldType[]
    {
        CarryManager.HeldType.CookedMeatBall,
    };



    private void Update()
    {

        if (playerInRange == false)
        {
            return;
        }

        if (Checkmeats() == true && playerInRange == true && playerController.interactAction.action.WasPressedThisFrame())
        {
            MakeBurger();
        }
    }


    private bool Checkmeats()
    {

        if (CarryManager.HoldingItem == false)
        {
            return false;
        }


        for (int i = 0; i < allowedTypes.Length; i++)
        {
            if (allowedTypes[i] == CarryManager.HeldItemType)
            {
                return true;
            }
        }

        return false;
    }

    private void MakeBurger()
    {

        if (CarryManager.HeldItem != null)
        {
            Destroy(CarryManager.HeldItem);
        }
        ReduceStamina();
        CarryManager.HeldItem = null;
        CarryManager.HeldItemType = CarryManager.HeldType.None;
        CarryManager.HoldingItem = false;

        Instantiate(Burger, burgerSpawnPoint.position,burgerSpawnPoint.rotation);
    }

    void ReduceStamina()
    {
        playerController.stamina = playerController.stamina - 10;
        playerController.staminaBar.value = playerController.stamina;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (Checkmeats() == true)
            {
                outlineMaterial.SetFloat("_Alpha", 1f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            outlineMaterial.SetFloat("_Alpha", 0f);
        }
    }
}
