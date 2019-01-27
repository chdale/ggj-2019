using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 transition = Vector3.Lerp(transform.position, player.transform.position, 5.0f * Time.deltaTime);
        transform.position = new Vector3(transition.x, transform.position.y, -10f);
    }
}
