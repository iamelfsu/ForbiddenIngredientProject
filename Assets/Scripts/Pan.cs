using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Pan : MonoBehaviour
{
    public FpsPlayerController playerController;
    public GameObject cookedMeatBall;
    public GameObject cookedMeatBall2;
    public GameObject vfxFire;
    public GameObject fActive;
    public GameObject Timer;
    public GameObject cookingBar;
    public GameObject spawnedA;
    public GameObject spawnedB;
    public Transform CmeatBallSpawnPointA;
    public Transform CmeatBallSpawnPointB;
    public Transform spawnFirePoint;
    public CookTimer CookTimer;
    
    
    private bool playerInRange;
    [SerializeField] private Material outlineMaterial;

    
    public CarryManager.HeldType[] allowedTypes = new CarryManager.HeldType[]
    {

        CarryManager.HeldType.Meatball
    };



    private void Start()
    {
        Timer.SetActive(false);
        cookingBar.SetActive(false);
    }

    private void Update()
    {

        if (playerInRange == false)
        {
            return;
        }

        if (CheckMeatball() == true && playerInRange == true && playerController.interactAction.action.WasPressedThisFrame())
        {
            MakeCooked();
        }
    }


    private bool CheckMeatball()
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

    private void MakeCooked()
    {

        if (CarryManager.HeldItem != null)
        {
            Destroy(CarryManager.HeldItem);
        }
        CarryManager.HeldItem = null;
        CarryManager.HeldItemType = CarryManager.HeldType.None;
        CarryManager.HoldingItem = false;

        spawnedA = Instantiate(cookedMeatBall, CmeatBallSpawnPointA.position, CmeatBallSpawnPointA.rotation);
        spawnedB = Instantiate(cookedMeatBall, CmeatBallSpawnPointB.position, CmeatBallSpawnPointB.rotation);
        fActive = Instantiate(vfxFire, spawnFirePoint.position, spawnFirePoint.rotation);
        Timer.SetActive(true);
        cookingBar.SetActive(true);

    }


    public void CookedM()
    {
        Destroy(spawnedA);
        Destroy(spawnedB);
        Destroy(fActive);
        Instantiate(cookedMeatBall2, CmeatBallSpawnPointA.position, CmeatBallSpawnPointA.rotation);
        Instantiate(cookedMeatBall2, CmeatBallSpawnPointB.position, CmeatBallSpawnPointB.rotation);
        Timer.SetActive(false);
        cookingBar.SetActive(false);
    }
   
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            CookTimer.countdownText.enabled = true;
            cookingBar.GetComponent<CanvasGroup>().alpha = 1f;

            if (CheckMeatball() == true)
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
            CookTimer.countdownText.enabled = false;
            cookingBar.GetComponent<CanvasGroup>().alpha = 0f;


            outlineMaterial.SetFloat("_Alpha", 0f);
        }
    }
}
