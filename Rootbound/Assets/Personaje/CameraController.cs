
using UnityEngine;


/// <summary>
/// Camera movement script for third person games.
/// This Script should not be applied to the camera! It is attached to an empty object and inside
/// it (as a child object) should be your game's MainCamera.
/// </summary>

public class CameraController : MonoBehaviour
{

    public float zoom = 30;
    public float sensitivity = 5f;

    public Vector2 cameraLimit = new Vector2(-45, 40);

    float mouseX;
    float mouseY;
    float offsetDistanceY;

    Transform player;

    void Start()
    {

        player = GameObject.FindWithTag("Player").transform;
        offsetDistanceY = transform.position.y;

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }


    void Update()
    {

        transform.position = player.position + new Vector3(0, offsetDistanceY, 0);

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
            zoom -= Input.GetAxis("Mouse ScrollWheel") * sensitivity * 2;
            Camera.main.fieldOfView = Mathf.Clamp(zoom, -40, 45);

        mouseX += Input.GetAxis("Mouse X") * sensitivity;
        mouseY += Input.GetAxis("Mouse Y") * sensitivity;

        mouseY = Mathf.Clamp(mouseY, cameraLimit.x, cameraLimit.y);

        transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0);

    }
}