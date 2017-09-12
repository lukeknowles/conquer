using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    public const int SPEED = 8;

    // Use this for initialization
    void Start ()
    {
        /*cam = gameObject.GetComponentInChildren<Camera>();

        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;*/
    }
	
	// Update is called once per frame
	void Update ()
    {
        /*float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotX += mouseY * SENSITIVITY * Time.deltaTime;
        rotY += mouseX * SENSITIVITY * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        gameObject.transform.rotation = localRotation;*/

	    if(Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Translate(new Vector3(0, 0, SPEED * Time.deltaTime));
        }

        if(Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(new Vector3(-SPEED * Time.deltaTime, 0, 0));
        }

        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Translate(new Vector3(0, 0, -SPEED * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(new Vector3(SPEED * Time.deltaTime, 0, 0));
        }
    }
}
