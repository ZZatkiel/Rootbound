using UnityEngine;

/*
 * SCRIPT DE ROTACION DE LA CAMARA Y EL SCROLL DEL FOV
 * ESTE SCRIPT COMPLEMENTA AL SCRIPT THIRDPERSONCONTROLLER
*/

public class CameraController : MonoBehaviour
{

    public float zoom;
    public float sensitivity = 5f;

    public Vector2 cameraLimit = new Vector2(-45, 40);

    float mouseX;
    float mouseY;
    float offsetDistanceY;

    Transform player;

    void Start()
    {

        zoom = Camera.main.fieldOfView;

        player = GameObject.FindWithTag("Player").transform;
        offsetDistanceY = transform.position.y;

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }


    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        transform.position = player.position + new Vector3(0, offsetDistanceY, 0);

        if (scroll != 0)
        {
            zoom -= scroll * sensitivity * 2;
            zoom = Mathf.Clamp(zoom, 40f, 90f);
            Camera.main.fieldOfView = zoom;


        }




        mouseX += Input.GetAxis("Mouse X") * sensitivity;
        mouseY += Input.GetAxis("Mouse Y") * sensitivity;

        mouseY = Mathf.Clamp(mouseY, cameraLimit.x, cameraLimit.y);

        transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0);

    }
}