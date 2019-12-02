using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotateSpeed = 1f;

    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime * rotateSpeed);
    }
}
