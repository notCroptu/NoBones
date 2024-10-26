using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{

    private GameManager _gameManager;
    private GameObject _collectButton;
    
    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _collectButton = transform.Find("CollectButton").gameObject;
        _collectButton.SetActive(false);
    }

    public void CollectThis()
    {
        gameObject.SetActive(false);
        _gameManager.GetData().AddSkull();
        Debug.Log("collected skull");
        Debug.Log($"Skulls: {_gameManager.GetData().GetSkulls()}");
    }

    public void InsideBounds()
    {
        _collectButton.SetActive(true);
    }

    public void OutsideBounds()
    {
        _collectButton.SetActive(false);
    }
    
}
