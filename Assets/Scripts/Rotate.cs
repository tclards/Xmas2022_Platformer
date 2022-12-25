using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float fSpeed = 2f;

    private void Update()
    {
        transform.Rotate(0, 0, 360 * fSpeed * Time.deltaTime);
    }
}
