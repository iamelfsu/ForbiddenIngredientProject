using System.Runtime.InteropServices;
using UnityEngine;

public class EquipBurger : MonoBehaviour
{
    public FpsPlayerController playerController;
    public GameObject Burger;
    public Transform BurgerParent;
    private bool isHeldByMe;
    private Rigidbody rb;
    private Collider burgerCollider;
    [SerializeField]private Material outlineMaterial;
    [SerializeField]private LayerMask interactableItems;
    private void Awake()
    {
        rb = Burger.GetComponent<Rigidbody>();
        burgerCollider = Burger.GetComponent<Collider>();
    }

    private void Start()
    {
        if (playerController == null)
        {
            playerController = FindFirstObjectByType<FpsPlayerController>();
        }
        if (BurgerParent == null)
        {
            BurgerParent = GameObject.Find("BurgerParent").transform;
        }
        rb.isKinematic = false;
        rb.useGravity = true;
        if (burgerCollider != null) burgerCollider.enabled = true;      
    }

    private void Update()
    {

        if (isHeldByMe && playerController.dropAction.action.WasPressedThisFrame())
        {
            Drop();
            return;
        }
   
        OnRayC();

    }

    void Equip()
    {
        isHeldByMe = true;
        CarryManager.HoldingItem = true;
        CarryManager.HeldItemType = CarryManager.HeldType.Burger;  
        CarryManager.HeldItem = Burger;
        rb.isKinematic = true;
        rb.useGravity = false;
        if (burgerCollider != null)
        {
            burgerCollider.enabled = false;
        }
        Burger.transform.SetParent(BurgerParent.transform);
        Burger.transform.SetPositionAndRotation(BurgerParent.position, BurgerParent.rotation);
    }
    void Drop()
    {
        isHeldByMe = false;
        CarryManager.HoldingItem = false;
        CarryManager.HeldItemType = CarryManager.HeldType.None;
        CarryManager.HeldItem = null;
        rb.isKinematic = false;
        rb.useGravity = true;
        if (burgerCollider != null)
        {
            burgerCollider.enabled = true;
        }
        Burger.transform.SetParent(null);
    }

    void OnRayC()
    {
        Ray ray = new Ray(playerController.pCam.transform.position, playerController.pCam.transform.forward);
        RaycastHit hit;

        outlineMaterial.SetFloat("_Alpha", 0f);
        if (Physics.Raycast(ray, out hit, 5f, interactableItems))
        {
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red);
            if (hit.collider.CompareTag("burger") && CarryManager.HoldingItem == false)
            {
                outlineMaterial.SetFloat("_Alpha", 1f);
                Debug.DrawRay(ray.origin, ray.direction * 5f, Color.green);

                if (playerController.interactAction.action.WasPressedThisFrame())
                {
                    Equip();

                }
                if(isHeldByMe && playerController.interactAction.action.WasPressedThisFrame())
                {
                    isEat();
                }
            }
        }
    }
    
    void isEat()
    {
        playerController.health = playerController.health - 10;
        playerController.healthBar.value = playerController.health;
        playerController.stamina = playerController.stamina + 33;
        playerController.staminaBar.value = playerController.stamina;
        Debug.Log("Eating Burger");
           Destroy(Burger);
    }
}

