using UnityEditor.Timeline.Actions;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AxeAnim : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Animator animator;
    public FpsPlayerController playerController;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.attackAction.action.IsPressed())
        {
            animator.SetTrigger("hit");
        }
    }
}
