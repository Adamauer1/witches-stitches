using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    // public const string ATTACKONE = "AttackOne";
    // public const string ATTACKTWO = "AttackTwo";
    // public const string ATTACKTHREE = "AttackThree";
    // public const string IDLE = "Idle";
    // public const string WALK = "Walking";
    // public const string JUMPSTART = "JumpStart";
    // public const string JUMPAPEX = "JumpApex";
    // public const string FALLING = "Falling";
    // public const string LANDING = "Landing";
    // public const string DEATH = "Death";


    private Animator animator;
    private string currentAnimaton;

    private void Start(){
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newAnimation){
        //Debug.Log("Changing animation to " + newAnimation);
        if (currentAnimaton == newAnimation){
            return;
        }

        animator.Play(newAnimation);
        currentAnimaton = newAnimation;
    }

    public float GetCurrentAnimationDuration(){
        return animator.GetCurrentAnimatorClipInfo(0).Length;
    }
}
