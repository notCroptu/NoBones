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
        if (other.CompareTag("Detect"))
        {
            Transform boneSpine = other.transform.parent.Find("BoneSpine");
            if (boneSpine == null) return;
            BoneManager boneManager2 = boneSpine.gameObject.GetComponentInParent<BoneManager>();
            if (!boneManager2.HasBeenFound())
            {
                boneManager2.Detected();
            }
            return;
        }
        if (!other.CompareTag("Bone")) return;
        BoneManager boneManager = other.gameObject.GetComponentInParent<BoneManager>();
        if (boneManager.IsTouched()) return;
        boneManager.Touched();
        if (!boneManager.Corrupted())
            _gameManager.AddScore();
        Instantiate(effectCollect, other.transform.position, other.transform.rotation);
    }
}
