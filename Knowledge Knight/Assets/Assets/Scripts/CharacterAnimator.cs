using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

[RequireComponent(typeof(SkeletonAnimation))]
public class CharacterAnimator : MonoBehaviour
{

    #region InspectorAnimations
    [SpineAnimation]
    public string walkAnimation;

    [SpineAnimation]
    public string idleAnimation;

    [SpineAnimation]
    public string runAnimation;

    [SpineAnimation]
    public string jumpAnimation;

    [SpineAnimation]
    public string hitAnimation;

    [SpineAnimation]
    public string deathAnimation;

    [SpineAnimation]
    public string pointAnimation;
    #endregion

    private SkeletonAnimation skeletonAnimation;

    private Spine.AnimationState animationState;
    private Spine.Skeleton skeleton;

    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        animationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.skeleton;
    }

    public void PlayWalkAnimation()
    {
        animationState.SetAnimation(0, walkAnimation, true);
    }

    public void PlayIdleAnimation()
    {
        animationState.SetAnimation(0, idleAnimation, true);
    }

    public void PlayPointAnimation()
    {
        animationState.SetAnimation(0, pointAnimation, false);
    }

    public void PlayDeathAnimation()
    {
        animationState.SetAnimation(0, deathAnimation, false);
    }

    public void PlayRunningAnimation()
    {
        animationState.SetAnimation(0, runAnimation, true);
    }

}
