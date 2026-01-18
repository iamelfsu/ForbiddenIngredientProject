using UnityEngine;
using static CarryManager;

public class EquipGmBall : MonoBehaviour
{
    public GameObject groundMeat;
    public Transform groundMpoint;
    public FpsPlayerController playerController;
    [SerializeField] private Material outlineMaterial;
    public LayerMask interactableItems;
    private bool isHeldByMe;

    private Rigidbody rb;
    private SphereCollider groundMeatCollider;

    private void Awake()
    {
        rb = groundMeat.GetComponent<Rigidbody>();
        groundMeatCollider = groundMeat.GetComponent<SphereCollider>();
    }

    private void Start()
    {
        if (playerController == null)
        {
            playerController = FindFirstObjectByType<FpsPlayerController>();
        }
        if (groundMpoint == null)
        {
            groundMpoint = GameObject.Find("groundMeatParent").transform;
        }
        rb.isKinematic = false;
        rb.useGravity = true;
        groundMeatCollider.enabled = true;


    }

    private void Update()
    {
        if (isHeldByMe == true && playerController.dropAction.action.WasPressedThisFrame())
        {
            Drop();
            return;
        }

        OnRayC();
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (CarryManager.HoldingItem && !isHeldByMe) return;

        playerInRange = true;

        if (CarryManager.FirstPickupInRange == null)
            CarryManager.FirstPickupInRange = this;
    }

    /*private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;

        if (CarryManager.FirstPickupInRange == (object)this)
            CarryManager.FirstPickupInRange = null;
    }*/

    private void Equip()
    {
        isHeldByMe = true;
        CarryManager.HoldingItem = true;
        CarryManager.HeldItemType = HeldType.Meatball;
        CarryManager.HeldItem = groundMeat;

        rb.isKinematic = true;
        rb.useGravity = false;

        groundMeatCollider.enabled = false;

        groundMeat.transform.SetParent(groundMpoint);
        groundMeat.transform.SetPositionAndRotation(groundMpoint.position, groundMpoint.rotation);

    }

    private void Drop()
    {
        isHeldByMe = false;
        CarryManager.HoldingItem = false;
        CarryManager.HeldItemType = HeldType.None;
        CarryManager.HeldItem = null;

        groundMeat.transform.SetParent(null);

        rb.isKinematic = false;
        rb.useGravity = true;

        groundMeatCollider.enabled = true;


    }

    void OnRayC()
    {
        Ray ray = new Ray(playerController.pCam.transform.position, playerController.pCam.transform.forward);
        RaycastHit hit;

        outlineMaterial.SetFloat("_Alpha", 0f);
        Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red);
        if (Physics.Raycast(ray, out hit, 5f, interactableItems))
        {
            if (hit.collider.CompareTag("RawMeat") && CarryManager.HoldingItem == false)
            {
                outlineMaterial.SetFloat("_Alpha", 1f);
                Debug.DrawRay(ray.origin, ray.direction * 5f, Color.green);

                if (playerController.interactAction.action.WasPressedThisFrame())
                {
                    Equip();

                }
            }

        }
    }
}