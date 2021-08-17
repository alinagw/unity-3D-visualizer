using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    public FixedJoystick joystick;
    public float translateSpeed;
    public float rotationSpeed;
    public float scaleSpeed;
    public float minScale;
    public float maxScale;

    public void TranslateModel()
    {
        Vector3 dir = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;
        transform.Translate(dir * translateSpeed * Time.deltaTime, Space.World);
    }

    public void RotateModel()
    {
        float angle = joystick.Horizontal * rotationSpeed;
        transform.RotateAround(transform.position, Vector3.up, angle);
    }

    public void ScaleModel()
    {
        float newScale = Mathf.Clamp(transform.localScale.x + (scaleSpeed * joystick.Vertical * Time.deltaTime), minScale, maxScale);
        transform.localScale = Vector3.one * newScale;
    }

    public void ResetPosition()
    {
        transform.position = Vector3.zero;
    }

    public void ResetRotation()
    {
        transform.rotation = Quaternion.identity;
    }

    public void ResetScale()
    {
        transform.localScale = Vector3.one;
    }

    public void ResetAllTransforms()
    {
        ResetPosition();
        ResetRotation();
        ResetScale();
    }

    // Update is called once per frame
    void Update()
    {
        // TranslateModel();
        // RotateModel();
        // ScaleModel();
    }
}
