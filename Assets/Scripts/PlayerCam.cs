using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] float xSensitivity = 400f;
    [SerializeField] float ySensitivity = 400f;

    public Transform orientation;

    private float xRotation;
    private float yRotation;

    [Header("Camera Shake Settings")]
    [SerializeField] float shakeIntensity = 0.7f;
    [SerializeField] float shakeDuration;
    private Vector3 cameraOriginalPos;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cameraOriginalPos = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        //ShakeCamera();
    }

    //private void ShakeCamera()
    //{
    //    this.transform.localPosition = cameraOriginalPos + Random.insideUnitSphere * shakeIntensity;
    //}
}
