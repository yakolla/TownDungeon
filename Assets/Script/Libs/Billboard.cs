﻿using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);

	}


}
