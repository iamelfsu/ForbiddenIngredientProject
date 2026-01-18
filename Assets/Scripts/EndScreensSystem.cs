using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;



public class EndScreensSystem : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;
    FpsPlayerController playerController;
    [SerializeField] private TextMeshProUGUI subTextW;
    [SerializeField] private TextMeshProUGUI subTextL;
    private string holder;
    private float writeSpeed = 0.1f;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void WinScreen()
    {
        StartCoroutine(Win());
    }

    void LoseScreen()
    {
        StartCoroutine(Lose());
    }
    IEnumerator Win()
    {
        winScreen.SetActive(true);
        playerController.enabled = false;
        yield return new WaitForSeconds(1f);
        holder = "YOU ESCAPED!";
        foreach (char c in holder)
        {
            subTextW.text += c;
            yield return new WaitForSeconds(writeSpeed);

        }
        yield return new WaitForSeconds(2f);
        winScreen.SetActive(false);
    }

    IEnumerator Lose()
    {
        loseScreen.SetActive(true);
        playerController.enabled = false;
        yield return new WaitForSeconds(1f);
        holder = "YOU DEAD!";
        foreach (char c in holder)
        {
            subTextL.text += c;
            yield return new WaitForSeconds(writeSpeed);

        }
        yield return new WaitForSeconds(2f);
        loseScreen.SetActive(false);
    }
}
