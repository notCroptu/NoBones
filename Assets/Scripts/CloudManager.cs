using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    void Start()
    {
        int numb = int.Parse(gameObject.name.Substring(5));
        GetComponent<Animator>().SetInteger("cloudNumb", numb);
    }
}
