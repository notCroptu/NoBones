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
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _spriteCorruptedRenderer;
    private bool _found = false;
    public bool HasBeenFound() => _found;
    [SerializeField] private Sprite[] sprites;
    
    void Start()
    {
        _gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;

        _spriteRenderer = transform.Find("SpriteObj").GetComponent<SpriteRenderer>();
        _spriteCorruptedRenderer = _spriteRenderer.transform.Find("CorruptedObj").GetComponent<SpriteRenderer>();
        _spriteCorruptedRenderer.enabled = false;
        
        if (Random.Range(0, 6) == 0) _corrupted = true;
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

    public void Detected()
    {
        _found = true;
        if (!_corrupted) return;
        _spriteRenderer.enabled = false;
        _spriteCorruptedRenderer.enabled = true;
        StartCoroutine(ChangeBone());
    }

    private IEnumerator ChangeBone()
    {
        int count = 0;
        while (true)
        {
            _spriteCorruptedRenderer.sprite = sprites[count];
            count++;
            if (count >= sprites.Length) break;
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    public IEnumerator DestroyBoneCauseIcan() {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
