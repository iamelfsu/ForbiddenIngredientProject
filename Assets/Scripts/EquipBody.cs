using UnityEngine;

public class EquipBody : MonoBehaviour
{
    public FpsPlayerController playerController;
    public GameObject Body;
    public GameObject BodyPieceA;
    public GameObject BodyPieceB;
    public Transform spawnPieceA;
    public Transform spawnPieceB;
    [SerializeField]private CarryManager.HeldType requiredItem = CarryManager.HeldType.Knife;
    private bool cutted;
    [SerializeField]private  Material outlineMaterial;
    [SerializeField]private LayerMask interactableItems;


    void Start()
    {
        if (playerController == null)
        {
            playerController = FindFirstObjectByType<FpsPlayerController>();
        }
        if(spawnPieceA == null)
        {
            spawnPieceA = GameObject.Find("SpawnPieceA").transform;
        }
        if (spawnPieceB == null)
        {
            spawnPieceB = GameObject.Find("SpawnPieceB").transform;
        }
        
    }
    private void Update()
    {
        if (cutted == true)
        {
            return;
        }

        OnRayC();
    }

    private void cut()
    {
        cutted = true;
        if(BodyPieceA)
        {
            Instantiate(BodyPieceA, spawnPieceA.position, spawnPieceA.rotation);
        }
        if(BodyPieceB)
        {
            Instantiate(BodyPieceB, spawnPieceB.position, spawnPieceB.rotation);
        }
        Destroy(gameObject);
    }


    void OnRayC()
    {
        Ray ray = new Ray(playerController.pCam.transform.position, playerController.pCam.transform.forward);
        RaycastHit hit;
        outlineMaterial.SetFloat("_Alpha", 0f);
        if (Physics.Raycast(ray, out hit, 5f, interactableItems))
        {   
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red);
            if (hit.collider.CompareTag("body") && CarryManager.HeldItemType == requiredItem)
            {
                Debug.DrawRay(ray.origin, ray.direction * 5f, Color.green);
                outlineMaterial.SetFloat("_Alpha", 1f);
                if (playerController.attackAction.action.WasPressedThisFrame())
                {
                    //add the animation here
                    cut();
                }
                else
                {
                    return;
                }
            }
        }
    }
}
