using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    private GameManager _gameManager;
    [SerializeField] private GameObject effectCollect;
    
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("PickUp")) return;
        BoneManager boneManager = other.gameObject.GetComponentInParent<BoneManager>();
        if (boneManager.IsTouched()) return;
        boneManager.Touched();
        _gameManager.AddScore();
        Instantiate(effectCollect, other.transform.position, other.transform.rotation);
    }
}
