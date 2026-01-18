using UnityEngine;
using static CarryManager;

public class EquipCookedM : MonoBehaviour
{
    public FpsPlayerController playerController;
    public GameObject cookedMeatBall;
    public Transform cookedMParent;
    private bool playerInRange;
    private bool isHeldByMe;
    private Rigidbody rb;
    private Collider cookedCollider;
    private void Awake()
    {
        rb = cookedMeatBall.GetComponent<Rigidbody>();
        cookedCollider = cookedMeatBall.GetComponent<Collider>();
    }

    private void Start()
    {
        if (playerController == null)
        {
            playerController = FindFirstObjectByType<FpsPlayerController>();
        }
        if (cookedMParent == null)
        {
            cookedMParent = GameObject.Find("CookedParent").transform;
        }
        rb.isKinematic = false;
        rb.useGravity = true;
        if (cookedCollider != null) cookedCollider.enabled = true;
    }

    private void Update()
    {
        if (CarryManager.HoldingItem && !isHeldByMe)
        {
            return;
        }
        if (isHeldByMe && playerController.dropAction.action.WasPressedThisFrame())
        {
            Drop();
            return;
        }
        if (!playerInRange)
        {
            return;
        }
        if (playerController.interactAction.action.WasPressedThisFrame())
        {
            if (CarryManager.HoldingItem) return;
            if (CarryManager.FirstPickupInRange != (object)this) return;
            Equip();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            CarryManager.FirstPickupInRange = this;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (CarryManager.FirstPickupInRange == (object)this)
            {
                CarryManager.FirstPickupInRange = null;
            }
        }
    }
    void Equip()
    {
        isHeldByMe = true;
        CarryManager.HoldingItem = true;
        CarryManager.HeldItemType = CarryManager.HeldType.CookedMeatBall;
        CarryManager.HeldItem = cookedMeatBall;
        rb.isKinematic = true;
        rb.useGravity = false;
        if (cookedCollider != null)
        {
            cookedCollider.enabled = false;
        }
        cookedMeatBall.transform.SetParent(cookedMParent.transform);
        cookedMeatBall.transform.SetPositionAndRotation(cookedMParent.position, cookedMParent.rotation);
    }
    void Drop()
    {
        isHeldByMe = false;
        CarryManager.HoldingItem = false;
        CarryManager.HeldItemType = CarryManager.HeldType.None;
        CarryManager.HeldItem = null;
        rb.isKinematic = false;
        rb.useGravity = true;
        if (cookedCollider != null)
        {
            cookedCollider.enabled = true;
        }
        cookedMeatBall.transform.SetParent(null);
    }
}

