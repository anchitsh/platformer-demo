using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    CharacterController chara;
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        chara = GetComponentInParent<CharacterController>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        print("HorizontalAxis"+chara.horizontalAxis);
        ani.SetFloat("HorizontalAxis", chara.horizontalAxis);
        ani.SetBool("jump", chara.jump);
        ani.SetInteger("jumpState", chara.midair);
    }
}
