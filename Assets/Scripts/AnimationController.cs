using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    CharacterController chara;
    Animator ani;

    void Start()
    {
        chara = GetComponentInParent<CharacterController>();
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalAxis = chara.horizontalAxis;
        print("HorizontalAxis"+chara.horizontalAxis);

        if (chara.onLeftWall && horizontalAxis<0)
        {
            horizontalAxis = 0;
        }
        else if (chara.onRightWall && horizontalAxis > 0)
        {
            horizontalAxis = 0;
        }

        ani.SetFloat("HorizontalAxis", horizontalAxis);
        ani.SetBool("jump", chara.jump);
        ani.SetInteger("jumpState", chara.midAir);
    }
}
