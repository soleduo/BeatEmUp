using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Camera mainCamera;
    public Transform target;
    public float distance = 24;
    public float yOffset = 1;

    [Range(0f, 1f)]
    public float damping = 0.3f;

    private void Start()
    {
        mainCamera.transform.position = TargetPosition;
    }

    private void Update()
    {
        CameraFollow();
    }

    private void CameraFollow()
    {
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, TargetPosition, damping);
    }

    private Vector3 TargetPosition
    {
        get { return target.transform.position + Vector3.up * yOffset + Vector3.back * distance; }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector3(TargetPosition.x, TargetPosition.y, 0), 0.3f);
    }
}
