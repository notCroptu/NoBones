using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float followRate;
    [SerializeField] private float yOffSet;
    [SerializeField] private bool lerpTrue;
    private Vector3 targetPosition;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = player.position;
        targetPosition.z = transform.position.z;
        Vector3 delta = targetPosition - transform.position;
        if (lerpTrue)
            transform.position += delta * followRate;
        transform.position = new Vector3(transform.position.x, transform.position.y + yOffSet, transform.position.z);
    }
}
