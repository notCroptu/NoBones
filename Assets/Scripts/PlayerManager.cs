using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    private GameManager _gameManager;
    [SerializeField] private GameObject _effectCollect;
    
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bone")) return;
        BoneManager boneManager = other.gameObject.GetComponent<BoneManager>();
        if (boneManager.IsTouched()) return;
        boneManager.Touched();
        _gameManager.AddScore();
        Instantiate(_effectCollect, other.transform.position, other.transform.rotation);
    }
}
