using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (GameManager.singleton.GameStarted)
            transform.position = transform.position + Vector3.forward * 5 * Time.fixedDeltaTime;
    }
}
