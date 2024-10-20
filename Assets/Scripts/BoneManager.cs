using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoneManager : MonoBehaviour
{

    private bool _corrupted = false;
    private Animator _animator;
    private bool _touched = false;
    public bool IsTouched() => _touched;
    private GameManager _gameManager;
    
    void Start()
    {
        _gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        
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
        if (_touched) return;
        _touched = true;
        if (_corrupted)
        {
            // DO BAD  STUFF, MAUAHAHHAHAHAH
            _gameManager.TakeDamage();
        }
        _animator.SetBool("collected", true);
        StartCoroutine(DestroyBoneCauseIcan());
    }
    private void OnTriggerEnter2D()
    {
        if (_corrupted)
        {

        }
    }
    private void OnTriggerExit2D()
    {
        if (_corrupted)
        {
            
        }
    }
    
    public IEnumerator DestroyBoneCauseIcan() {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
