using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAnimationBehaviour : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        animator.SetFloat("MoveSpeed", GetComponent<CharacterController>().velocity.magnitude);
    }
}
