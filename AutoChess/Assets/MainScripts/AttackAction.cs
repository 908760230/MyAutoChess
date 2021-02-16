using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.transform.parent.GetComponent<ChessAnimation>().OnAttackAnimationFinished();
    }
}
