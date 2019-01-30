//----------------------------------------------
//            Pressure Wacom Wrapper for Unity
// Copyright © 2013 Pierre Gufflet
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PressureLib;


public class pressureExampleCube : MonoBehaviour {	
	
	// Use this for initialization
	void Start () {
		if (PressureManager.instance==null) Debug.LogError("PressureManager not found, please drag the prefab to your hierarchy");
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 v = Camera.main.ScreenToWorldPoint(new Vector3(PressureManager.cursorPosition.x, PressureManager.cursorPosition.y, -Camera.main.transform.position.z)); 
		gameObject.transform.position = v;
		
		float scale = PressureManager.normalPressure * 3;
		
		gameObject.transform.localScale = new Vector3(scale,scale,scale);
		gameObject.transform.rotation = Quaternion.Euler(Mathf.Lerp(-45, 45, (PressureManager.tilt.y + 1) * 0.5f ),Mathf.Lerp(-45, 45, (PressureManager.tilt.x + 1) * 0.5f),0);
		
		if (PressureManager.eraser) gameObject.GetComponent<Renderer>().material.color = Color.red;
		else gameObject.GetComponent<Renderer>().material.color = Color.green;
	}
	
	

}
