using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private Dictionary<MenuItems, GameObject> _menuItems = new Dictionary<MenuItems, GameObject>();
    private List<Vector3> _menuPositions = new List<Vector3>();
    private Canvas _canvas;
    private MenuItems _highlightedOption = MenuItems.Play;
    private bool _isMoving = false;
    private Transform _buttonsObject;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
        _buttonsObject = _canvas.transform.Find("Panel").Find("MenuItems");
        LoadMenuItems();
    }

    private void LoadMenuItems()
    {
        int count = 0;
        foreach (MenuItems item in Enum.GetValues(typeof(MenuItems)))
        {
            if (item == MenuItems.Ignore) continue;
            string nameItem = "Button" + item.ToString();
            Transform child = _buttonsObject.Find(nameItem);
            if (count < 3)
            {
                _menuPositions.Add(child.localPosition);
            }
            _menuItems.Add(item, child.gameObject);
            child.gameObject.SetActive(false);
            count++;
        }
        
        int indexShown = (int)_highlightedOption - 1;
        int done = 0;
        foreach (MenuItems item in Enum.GetValues(typeof(MenuItems)))
        {
            if (item == MenuItems.Ignore) continue;
            GameObject buttonObj = _menuItems[item];
            if (done >= 3)
            {
                buttonObj.gameObject.SetActive(false);
                break;
            }
            if ((int)item < indexShown) continue;
            buttonObj.transform.localPosition = _menuPositions[done];
            buttonObj.gameObject.SetActive(true);
            Color color = new Color(0.4f, 0.4f, 0.4f, 1.0f);
            if (item == _highlightedOption) color = new Color(0.3f, 0.7f, 0.2f, 1.0f);
            buttonObj.GetComponent<Image>().color = color;
            done++;
        }

    }

    void Update()
    {
        // select middle option always
        // key down - go down the list
        // key up - go up the list
        if (_isMoving) return;
        bool clicked = false;
        bool goDown = true;
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && (int)_highlightedOption < 3)
        {
            _highlightedOption = GetOption((int)_highlightedOption + 1);
            clicked = true;
        }

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && (int)_highlightedOption > 0)
        {
            _highlightedOption = GetOption((int)_highlightedOption - 1);
            clicked = true;
            goDown = false;
        }
        if (clicked) UpdateOptionsPart(goDown);
    }

    private void UpdateOptionsPart(bool goDown = true)
    {
        _isMoving = true;
        StartCoroutine(AfterAnimation());
    }

    private IEnumerator MoveUpAnimation()
    {
        List<GameObject> objectsToMove = new List<GameObject>();
        foreach (Transform trans in _buttonsObject.transform)
        {
            if (!trans.gameObject.activeSelf) continue;
            objectsToMove.Add(trans.gameObject);
            if (objectsToMove.Count >= 4) break;
        }

        while (true)
        {
            foreach (GameObject obj in objectsToMove)
            {
                Vector3 current = obj.transform.localPosition;
                current.y += 5;
            }
            HideLilBitObj(objectsToMove[0]);
            ShowLilBitObj(objectsToMove[3]);
            yield return new WaitForSeconds(0.02f);
        }
    }

    private void ShowLilBitObj(GameObject obj)
    {
        Image img = obj.GetComponent<Image>();
        Color col = img.color;
        if (col.a < 1) col.a = col.a += 0.05f;
        img.color = col;
        TMP_Text tmp = obj.transform.Find("Text").GetComponent<TMP_Text>();
        Color tcol = img.color;
        if (tcol.a < 1) tcol.a = tcol.a += 0.05f;
        tmp.color = tcol;
    }
    
    private void HideLilBitObj(GameObject obj)
    {
        Image img = obj.GetComponent<Image>();
        Color col = img.color;
        col.a = col.a -= 0.05f;
        img.color = col;
        TMP_Text tmp = obj.transform.Find("Text").GetComponent<TMP_Text>();
        Color tcol = img.color;
        tcol.a = tcol.a -= 0.05f;
        tmp.color = tcol;
    }
    
    private IEnumerator AfterAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        _isMoving = false;
        int indexShown = (int)_highlightedOption - 1;
        int done = 0;
        foreach (MenuItems item in Enum.GetValues(typeof(MenuItems)))
        {
            if (item == MenuItems.Ignore) continue;
            GameObject buttonObj = _menuItems[item];
            if (done >= 3)
            {
                buttonObj.gameObject.SetActive(false);
                break;
            }
            if ((int)item < indexShown) continue;
            buttonObj.transform.localPosition = _menuPositions[done];
            buttonObj.gameObject.SetActive(true);
            Color color = new Color(0.4f, 0.4f, 0.4f, 1.0f);
            if (item == _highlightedOption) color = new Color(0.3f, 0.7f, 0.2f, 1.0f);
            buttonObj.GetComponent<Image>().color = color;
            done++;
        }
    }
    
    private MenuItems GetOption(int value)
    {
        foreach (MenuItems item in Enum.GetValues(typeof(MenuItems)))
        {
            if ((int)item == value) return item;
        }

        return MenuItems.Ignore;
    }
    
    private enum MenuItems
    {
        Play, Options, Credits, Quit, Ignore
    }
}
