using UnityEngine;
using static CarryManager;
using static UnityEngine.ParticleSystem;

public class EquipHand : MonoBehaviour
{
    [SerializeField] private GameObject Hand;
    public Transform HandParent;
    public FpsPlayerController playerController;
    private bool isHeldByMe;
    [SerializeField]private  Material outlineMaterial;
    [SerializeField]private LayerMask interactableItems;

    private Rigidbody rb;
    private MeshCollider handCollider;

    private void Awake()
    {
        rb = Hand.GetComponent<Rigidbody>();
        handCollider = Hand.GetComponent<MeshCollider>();
    }

    private void Start()
    {
        if (playerController == null)
        {
            playerController = FindFirstObjectByType<FpsPlayerController>();
        }
        if (HandParent == null)
        {
            HandParent = GameObject.Find("HandParent").transform;
        }
        rb.isKinematic = false;
        rb.useGravity = true;

        if (HandParent != null) handCollider.enabled = true;
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

    void OnRayC()
    {
        Ray ray = new Ray(playerController.pCam.transform.position, playerController.pCam.transform.forward);
        RaycastHit hit;

        outlineMaterial.SetFloat("_Alpha", 0f);
        if (Physics.Raycast(ray, out hit, 5f, interactableItems))
        {
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red);
            if (hit.collider.CompareTag("hand") && CarryManager.HoldingItem == false)
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



    private void Equip()
    {
        isHeldByMe = true;
        CarryManager.HoldingItem = true;
        CarryManager.HeldItemType = HeldType.Hand;
        CarryManager.HeldItem = Hand;

        rb.isKinematic = true;
        rb.useGravity = false;

        if (handCollider != null)
        {
            handCollider.enabled = false;
        }


        Hand.transform.SetParent(HandParent);
        Hand.transform.SetPositionAndRotation(HandParent.position, HandParent.rotation);

    }

    private void Drop()
    {
        isHeldByMe = false;
        CarryManager.HoldingItem = false;
        CarryManager.HeldItemType = HeldType.None;
        CarryManager.HeldItem = null;

        Hand.transform.SetParent(null);

        rb.isKinematic = false;
        rb.useGravity = true;

        if (handCollider != null) handCollider.enabled = true;


    }
}
