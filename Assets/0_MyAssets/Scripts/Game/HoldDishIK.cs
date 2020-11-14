using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// https://gametukurikata.com/animationanimator/bringik
/// </summary>
public class HoldDishIK : MonoBehaviour
{
    [SerializeField] Transform leftHandTransform;
    [SerializeField] Transform rightHandTransform;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
        //　左手、右手のIK設定
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTransform.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTransform.rotation);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTransform.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTransform.rotation);
    }
}
