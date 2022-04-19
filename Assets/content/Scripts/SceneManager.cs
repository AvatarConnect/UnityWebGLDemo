using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject Platform;
    private Vector3 _DefaultPlatformSize;
    private float _DefaultRotation;
    private float _AngularVelocity;
    private float _AngularVelocityDamping = 1f;
    private float _CameraZoom;

    void Awake()
    {
        _DefaultRotation = Platform.transform.rotation.eulerAngles.y;
        _DefaultPlatformSize = Platform.transform.localScale;
        _CameraZoom = Camera.main.fieldOfView;
    }

    void Update()
    {
        // Get mouse input
        if (Input.GetMouseButton(0))
        {
            _AngularVelocity -= Input.GetAxis("Mouse X") * 5;
            Platform.transform.localScale = _DefaultPlatformSize * 0.9f;
        }

        if (AvatarConnect.Core.ServiceInitialized && AvatarConnect.Core.Avatars.Count > 0)
        {
            // Get keyboard input
            if (Input.GetKey(KeyCode.W))
            {
                AvatarConnect.Core.Avatars[0].Animator.SetFloat("Speed", 2);
            }
            else
            {
                AvatarConnect.Core.Avatars[0].Animator.SetFloat("Speed", 0);
            }
        }

        // Zoom camera
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView * (1 + Input.GetAxis("Mouse ScrollWheel") * 1f), 10, 100);

        Platform.transform.localScale = Vector3.Lerp(Platform.transform.localScale, _DefaultPlatformSize, 2 * Time.deltaTime);
        Platform.transform.rotation = Quaternion.Euler(0, _DefaultRotation + _AngularVelocity, 0);
    }
}
