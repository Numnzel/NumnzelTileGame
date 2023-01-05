using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    Animator animator;
    //ZombieController zombieController;
    public bool canRotate;

    public void Initialize()
    {
        //zombieController = GetComponent<ZombieController>();
        animator = GetComponent<Animator>();
    }

    public void UpdateAnimatorValues(float speed)
    {
        #region speed
        float spd = 0;

        if (speed > 0 && speed < 0.55f) spd = 0.5f;
        else if (speed > 0.55f) spd = 1;
        else if (speed < 0 && speed > -0.55f) spd = -0.5f;
        else if (speed < -0.55f) spd = -1;

        animator.SetFloat("Speed", spd, 0.01f, Time.deltaTime);
        #endregion
    }

    public void PlayTargetAnimation(string animationName, bool isInteracting)
    {
        animator.applyRootMotion = isInteracting;
        animator.SetBool("IsInteracting", isInteracting);
        animator.CrossFade(animationName, 0.2f);
    }

    public void CanRotate()
    {
        canRotate = true;
    }

    public void StopRotation()
    {
        canRotate = false;
    }

    private void OnAnimatorMove()
    {
        //if (!zombieController.isInteracting)
        //{
        //    return;
        //}
        
        float timeDelta = Time.deltaTime;
        //Rigidbody RB = zombieController.GetComponent<Rigidbody>();

        //RB.drag = 0;
        Vector3 deltaPosition = animator.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / timeDelta;
        //RB.velocity = velocity;
    }
}
