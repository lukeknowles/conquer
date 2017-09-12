using UnityEngine;
using System.Collections;

public class TextTrackCam : MonoBehaviour
{
    public Camera cam;

	void Start ()
    {
        cam = GameObject.Find("Player").GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update ()
    {

        Vector3 v = cam.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(cam.transform.position - v);
        transform.Rotate(0, -180, 180);
    }
}
