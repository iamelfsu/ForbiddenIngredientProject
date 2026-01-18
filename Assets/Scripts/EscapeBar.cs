using UnityEngine;
using UnityEngine.UI;

public class EscapeBar : MonoBehaviour
{
    public Slider escapeBar;
    public int maxValue = 200;
    public int damage = 2;

    // checklist: bu sayýlara gelince tetikle
    public int[] checkpoints = new int[] { 178, 155, 80, 36 };

    private bool[] checkpointDone; // her checkpoint için done listesi
    private int health;

    void Start()
    {
        // slider ayarý
        escapeBar.maxValue = maxValue;
        escapeBar.value = maxValue;

        // can ve checklist reset
        health = maxValue;
        checkpointDone = new bool[checkpoints.Length];
        for (int i = 0; i < checkpointDone.Length; i++)
            checkpointDone[i] = false;
    }

    public void HitWall()
    {
        if (health <= 0) return;

        health = health - damage;
        if (health < 0) health = 0;

        escapeBar.value = health;


        for (int i = 0; i < checkpoints.Length; i++)
        {
            if (checkpointDone[i] == true) continue;

            if (health <= checkpoints[i])
            {
                checkpointDone[i] = true; 
                Debug.Log("Checkpoint reached: " + checkpoints[i]);
            }
        }

        if (health == 0)
        {
            Debug.Log("WALL BROKEN!");

        }
    }

    public void ResetBar()
    {
        health = maxValue;
        escapeBar.value = health;

        for (int i = 0; i < checkpointDone.Length; i++)
            checkpointDone[i] = false;
    }
}
