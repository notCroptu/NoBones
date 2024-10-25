using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class CatText : MonoBehaviour
{
    private TMP_Text tmp;

    void Start()
    {
        tmp = transform.Find("Text").GetComponent<TMP_Text>();
        //StartCoroutine(ChangeTextCat());
    }

    private IEnumerator ChangeTextCat()
    {
        yield return new WaitForSeconds(1.0f);
        while (true)
        {
            StartCoroutine(ChangeText("Hello!", 1.0f));
        }
    }

    private IEnumerator ChangeText(string text, float time)
    {
        int count = 0;
        while (true)
        {
            count++;
            tmp.text += text.Substring(count-1, count);
            yield return new WaitForSeconds(time);
        }
    }

}
