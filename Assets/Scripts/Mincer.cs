using UnityEngine;

public class Mincer : MonoBehaviour
{
    public FpsPlayerController playerController;
    public GameObject groundMeat;
    public Transform groundMsP;
    private bool playerInRange;
    [SerializeField]private Material outlineMaterial;

    public CarryManager.HeldType[] allowedTypes = new CarryManager.HeldType[]
    {
        CarryManager.HeldType.Hand,
        CarryManager.HeldType.Leg,
        CarryManager.HeldType.Head,
        CarryManager.HeldType.BodyPieceA,
        CarryManager.HeldType.BodyPieceB
    };



    private void Update()
    {

        if (playerInRange == false)
        {
            return;
        }

        if (CheckIfCanMince() == true && playerInRange == true && playerController.interactAction.action.WasPressedThisFrame())
        {
            MakeMeatBall();
        }
    }

    
    private bool CheckIfCanMince()
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

    private void MakeMeatBall()
    {

        if (CarryManager.HeldItem != null)
        {
            Destroy(CarryManager.HeldItem);
        }
        ReduceStamina();
        CarryManager.HeldItem = null;
        CarryManager.HeldItemType = CarryManager.HeldType.None;
        CarryManager.HoldingItem = false;

        Instantiate(groundMeat, groundMsP.position, groundMsP.rotation);
    }

    void ReduceStamina()
    {
            playerController.health = playerController.health -10;
            playerController.healthBar.value = playerController.health;
    }
    private void OnTriggerEnter(Collider other)
    {
 
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (CheckIfCanMince() == true)
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