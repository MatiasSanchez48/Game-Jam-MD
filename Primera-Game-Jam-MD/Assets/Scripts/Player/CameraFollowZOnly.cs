using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowZOnly : MonoBehaviour
{
    public Transform target;       // Player
    public float followSpeed = 5f;

    private float fixedX;
    private float fixedY;
    private float offsetZ;

    void Start()
    {
        fixedX = transform.position.x;
        fixedY = transform.position.y;
        offsetZ = transform.position.z - target.position.z;
    }

    void LateUpdate()
    {
        Vector3 targetPos = new Vector3(
            fixedX,                          // ❌ no sigue lados
            fixedY,                          // ❌ no sube/baja (podés cambiar)
            target.position.z + offsetZ      // ✅ sigue adelante / atrás
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            followSpeed * Time.deltaTime
        );
    }

    public void SnapToPosition(Transform newPosition)
    {
        transform.position = newPosition.position;

        fixedX = newPosition.position.x;
        fixedY = newPosition.position.y;
        offsetZ = newPosition.position.z - target.position.z;

        transform.rotation = newPosition.rotation;
    }
}
