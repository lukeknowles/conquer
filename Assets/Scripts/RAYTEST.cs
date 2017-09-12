using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAYTEST : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x + 1500, transform.position.y, transform.position.z), Color.red, 100f); // RIGHT
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x - 1500, transform.position.y, transform.position.z), Color.green, 100f); // LEFT
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x + 750, transform.position.y, transform.position.z + 1250), Color.yellow, 100f); // LEFT
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x - 750, transform.position.y, transform.position.z - 1250), Color.magenta, 100f); // LEFT
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x + 700, transform.position.y, transform.position.z - 1000), Color.cyan, 100f); // LEFT
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x - 700, transform.position.y, transform.position.z + 1000), Color.white, 100f); // LEFT
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
