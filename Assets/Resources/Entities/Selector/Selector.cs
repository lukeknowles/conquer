using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        gameObject.name = "Selector";
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 350, Space.World);
    }
}
