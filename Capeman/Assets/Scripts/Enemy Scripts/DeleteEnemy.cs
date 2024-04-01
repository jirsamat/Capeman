using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteEnemy : StateMachineBehaviour
{
    //deletes the enemy after it is killed and it's death anim. is played
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject.transform.parent.gameObject, stateInfo.length);
    }
}
