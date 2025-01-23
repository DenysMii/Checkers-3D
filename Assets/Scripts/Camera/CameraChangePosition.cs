using System;
using UnityEngine;

public class CameraChangePosition : MonoBehaviour
{
    
    [Header("Target Object")]
    [SerializeField] private Transform target;

    [Header("Sensitivity")]
    [SerializeField] private float verticalRotationSensitivity;
    [SerializeField] private float horizontalRotationSensitivity;
    [SerializeField] private float zoomSensitivity;

    [Header("Rotation")]
    [SerializeField] private float minVerticalAngle;
    [SerializeField] private float maxVerticalAngle;
    [Space(5)]
    [SerializeField] private float minHorizontalAngle;
    [SerializeField] private float maxHorizontalAngle;

    [Header("Distance")]
    [SerializeField] private float startDistance;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;

    private float horizontalAngle;
    private float verticalAngle;
    private float distance;

    private void Awake()
    {
        verticalAngle = transform.rotation.eulerAngles.x;
        horizontalAngle = transform.rotation.eulerAngles.y;
        distance = startDistance;
        
        UpdatePosition();
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButton(1))
            RotateAround();

        Zoom();
        UpdatePosition();
    }

    private void RotateAround()
    {
        float horizontalInput = Input.GetAxis("Mouse X"); // Horizontal mouse movement
        float verticalInput = Input.GetAxis("Mouse Y"); // Vertical mouse movement

        horizontalAngle += horizontalInput * horizontalRotationSensitivity * Time.deltaTime;
        verticalAngle -= verticalInput * verticalRotationSensitivity * Time.deltaTime;

        horizontalAngle = Mathf.Clamp(horizontalAngle, minHorizontalAngle, maxHorizontalAngle);
        verticalAngle = Mathf.Clamp(verticalAngle, minVerticalAngle, maxVerticalAngle);
    }

    private void Zoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance = Mathf.Clamp(distance - scroll * zoomSensitivity, minDistance, maxDistance);
    }

    private void UpdatePosition()
    {
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(verticalAngle, horizontalAngle, 0);
        transform.position = target.position + rotation * direction;
        transform.LookAt(target);
    }
}
