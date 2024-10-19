using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    [field:SerializeField] public AudioClip Die { get; private set; }
    [field:SerializeField] public AudioClip Net { get; private set; }
    [field:SerializeField] public AudioClip Vassoura { get; private set; }
}