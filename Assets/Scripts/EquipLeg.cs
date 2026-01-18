using UnityEngine;
using static CarryManager;

public class EquipLeg : MonoBehaviour
{
    public GameObject Leg;
    public Transform LegParent;
    public FpsPlayerController playerController;
    private bool isHeldByMe;

    private Rigidbody rb;
    private MeshCollider handCollider;
    [SerializeField]private Material outlineMaterial;   
    [SerializeField]private LayerMask interactableItems;
    private void Awake()
    {
        rb = Leg.GetComponent<Rigidbody>();
        handCollider = Leg.GetComponent<MeshCollider>();
    }

    private void Start()
    {
        if (playerController == null)
        {
            playerController = FindFirstObjectByType<FpsPlayerController>();
        }
        if (LegParent == null)
        {
            LegParent = GameObject.Find("LegParent").transform;
        }
        rb.isKinematic = false;
        rb.useGravity = true;

        if (handCollider != null) handCollider.enabled = true;
        var mr = Leg.GetComponent<MeshRenderer>();
        if (mr != null) mr.enabled = true;
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


    private void Equip()
    {
        isHeldByMe = true;
        CarryManager.HoldingItem = true;
        CarryManager.HeldItemType = HeldType.Leg;
        CarryManager.HeldItem = Leg;

        rb.isKinematic = true;
        rb.useGravity = false;

        if (handCollider != null)
        {
            handCollider.enabled = false;
        }


        Leg.transform.SetParent(LegParent);
        Leg.transform.SetPositionAndRotation(LegParent.position, LegParent.rotation);

    }

    private void Drop()
    {
        isHeldByMe = false;
        CarryManager.HoldingItem = false;
        CarryManager.HeldItemType = HeldType.None;
        CarryManager.HeldItem = null;

        Leg.transform.SetParent(null);

        rb.isKinematic = false;
        rb.useGravity = true;

        if (handCollider != null) handCollider.enabled = true;

        CarryManager.FirstPickupInRange = null;

    }

    void OnRayC()
    {
        Ray ray = new Ray(playerController.pCam.transform.position, playerController.pCam.transform.forward);
        RaycastHit hit;

        outlineMaterial.SetFloat("_Alpha", 0f);
        if (Physics.Raycast(ray, out hit, 5f, interactableItems))
        {
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red);
            if (hit.collider.CompareTag("leg") && CarryManager.HoldingItem == false)
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
