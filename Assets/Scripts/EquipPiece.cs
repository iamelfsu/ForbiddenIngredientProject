using Unity.Cinemachine;
using UnityEngine;
using static CarryManager;

public class EquipPiece : MonoBehaviour
{
    [SerializeField]private GameObject BodyPieceA;
    public Transform BodyPieceParent;
    public FpsPlayerController playerController;
    private bool isHeldByMe;
    [SerializeField]private Material outlineMaterial;
    [SerializeField]private LayerMask interactableItems;

    private Rigidbody rb;
    private CapsuleCollider bodyCollider;

    private void Awake()
    {
        rb = BodyPieceA.GetComponent<Rigidbody>();
        bodyCollider = BodyPieceA.GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        if (playerController == null)
        {
            playerController = FindFirstObjectByType<FpsPlayerController>();
        }
        if (BodyPieceParent == null)
        {
            BodyPieceParent = GameObject.Find("BodyPieceParent").transform;
        }
        rb.isKinematic = false;
        rb.useGravity = true;

        if (bodyCollider != null) bodyCollider.enabled = true;

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
        CarryManager.HeldItemType = HeldType.BodyPieceA; 
        CarryManager.HeldItem = BodyPieceA;

        rb.isKinematic = true;
        rb.useGravity = false;

        if (bodyCollider != null) bodyCollider.enabled = false;

            transform.SetParent(BodyPieceParent);
            transform.SetPositionAndRotation(BodyPieceParent.position, BodyPieceParent.rotation);

    }

    private void Drop()
    {
        isHeldByMe = false;
        CarryManager.HoldingItem = false;
        CarryManager.HeldItemType = HeldType.None;
        CarryManager.HeldItem = null;

        transform.SetParent(null);

        rb.isKinematic = false;
        rb.useGravity = true;

        if (bodyCollider != null) bodyCollider.enabled = true;


    }
    void OnRayC()
    {
        Ray ray = new Ray(playerController.pCam.transform.position, playerController.pCam.transform.forward);
        RaycastHit hit;

        outlineMaterial.SetFloat("_Alpha", 0f);
        if (Physics.Raycast(ray, out hit, 5f, interactableItems))
        {
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red);
            if (hit.collider.CompareTag("bodyPiece") && CarryManager.HoldingItem == false)
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
