using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoneManager : MonoBehaviour
{

    private bool _corrupted = false;
    [SerializeField] private Animator _animator;
    private bool _touched = false;
    public bool IsTouched() => _touched;
    private GameManager _gameManager;
    private bool inRange = false;
    private SpriteRenderer spriteRenderer;
    [SerializeField] Sprite corruptSprite;
    [SerializeField] Sprite normalSprite;
    
    void Start()
    {
        _gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;

        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        
        if (Random.Range(0, 1) == 0)
        {
            _corrupted = true;
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Detect"))
        {
            if (_corrupted)
            {
                _animator.SetTrigger("Corrupt");
                inRange = true;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Detect"))
        {
            if (_corrupted)
            {
                inRange = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Detect"))
        {
            if (_corrupted)
            {
                _animator.SetTrigger("Uncorrupt");
                inRange = false;
            }
        }
    }
    public void CheckCorrectIdleSprite()
    {
        if (inRange)
            spriteRenderer.sprite = corruptSprite;
        else if (_corrupted)
            spriteRenderer.sprite = normalSprite;
    }
    
    public IEnumerator DestroyBoneCauseIcan() {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
