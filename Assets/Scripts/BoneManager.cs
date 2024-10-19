using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneManager : MonoBehaviour
{

    private bool _corrupted = false;
    private Animator _animator;
    
    void Start()
    {
        _animator = transform.Find("Sprite").GetComponent<Animator>();
        if (Random.Range(0, 5) == 0)
        {
            _corrupted = true;
            SpriteRenderer spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = Resources.Load<Sprite>("SkeletonHeadSideView");
            // if corrupted
            // update sprite to corrupted bone sprite
            
            // corrupted = purple/pink glow
            // default = white glow
        }
        
    }

    public void Touched()
    {
        if (_corrupted)
        {
            // DO BAD  STUFF, MAUAHAHHAHAHAH
        }
        _animator.SetBool("collected", true);
    }
    
}
