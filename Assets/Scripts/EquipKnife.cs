using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static CarryManager;

public class EquipKnife : MonoBehaviour
{
    public GameObject Knife;
    public Transform KnifeParent;
    public FpsPlayerController playerController;
    public LayerMask interactableItems;

    //private bool playerInRange;
    private bool isHeldByMe;

    private Rigidbody rb;
    private BoxCollider physicsCollider;
    [SerializeField]private Material outlineMaterial;

    private void Awake()
    {
        rb = Knife.GetComponent<Rigidbody>();
        physicsCollider = Knife.GetComponent<BoxCollider>(); 
    }

    private void Start()
    {
        rb.isKinematic = true;
        rb.useGravity = false;
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
        CarryManager.HeldItemType = HeldType.Knife;
        CarryManager.HeldItem = Knife;

        rb.isKinematic = true;
        rb.useGravity = false;

        if (physicsCollider != null) physicsCollider.enabled = false;

        Knife.transform.SetParent(KnifeParent);
        Knife.transform.SetPositionAndRotation(KnifeParent.position, KnifeParent.rotation);
    }

    private void Drop()
    {
        isHeldByMe = false;
        CarryManager.HoldingItem = false;
        CarryManager.HeldItemType = HeldType.None;
        CarryManager.HeldItem = null;

        Knife.transform.SetParent(null);

        rb.isKinematic = false;
        rb.useGravity = true;

        if (physicsCollider != null) physicsCollider.enabled = true;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = true;
        if (CarryManager.HoldingItem == true)
        {
            return;
        }
        else
        {
            outlineMaterial.SetFloat("_Alpha", 1f);
        }
    }

    /*private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = false;
        outlineMaterial.SetFloat("_Alpha", 0f);
    }*/

    /*void OnRayC()
    {
        Ray ray = new Ray(playerController.pCam.transform.position, playerController.pCam.transform.forward);
        RaycastHit hit;

        outlineMaterial.SetFloat("_Alpha", 0f);
        if (Physics.Raycast(ray, out hit, 5f, interactableItems))
        {
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red);
        
            if (hit.collider.CompareTag("knife"))
            {
                outlineMaterial.SetFloat("_Alpha", 1f);
                Debug.DrawRay(ray.origin, ray.direction * 5f, Color.green);

                if (playerController.interactAction.action.WasPressedThisFrame())
                {
                    if (CarryManager.HoldingItem)
                    {
                        return;
                    }
                    else
                    {
                        Equip();

                    }
                }
            }

        }

    }*/
    void OnRayC()
    {
        Ray ray = new Ray(playerController.pCam.transform.position, playerController.pCam.transform.forward);
        RaycastHit hit;

        outlineMaterial.SetFloat("_Alpha", 0f);

        if (Physics.Raycast(ray, out hit, 5f, interactableItems))
        {
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red);
            if (hit.collider.CompareTag("knife") && CarryManager.HoldingItem == false)
            {
                outlineMaterial.SetFloat("_Alpha", 1f);
                Debug.DrawRay(ray.origin, ray.direction * 5f, Color.green);

                if (playerController.interactAction.action.WasPressedThisFrame())
                {          
                    Equip();
                }
                else
                {
                    return;
                }
            }
        }
    }
}
