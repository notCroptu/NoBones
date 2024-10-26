using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField] private GameObject effectCollect;
    private List<GameObject> _collectableObjects = new List<GameObject>();
    
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) ClickedToCollect();

    }

    private void ClickedToCollect()
    {
        if (_collectableObjects.Count == 0) return;
        GameObject[] list = _collectableObjects.ToArray();
        foreach (GameObject collectableObject in list)
        {
            CollectableManager manager = collectableObject.GetComponent<CollectableManager>();
            manager.CollectThis();
            _collectableObjects.Remove(collectableObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Collectable"))
        {
            CollectableManager collectableManager = other.GetComponent<CollectableManager>();
            collectableManager.OutsideBounds();
            if (_collectableObjects.Contains(collectableManager.gameObject))
            {
                _collectableObjects.Remove(collectableManager.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectable"))
        {
            CollectableManager collectableManager = other.GetComponent<CollectableManager>();
            collectableManager.InsideBounds();
            _collectableObjects.Add(collectableManager.gameObject);
            return;
        }
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
