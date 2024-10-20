using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giveValuesToAnimator : MonoBehaviour
{
    private BoneManager bm;
    void Start()
    {
        bm = GetComponentInParent<BoneManager>();
    }

    public void UpdateIdle()
    {
        bm.CheckCorrectIdleSprite();
    }
}
