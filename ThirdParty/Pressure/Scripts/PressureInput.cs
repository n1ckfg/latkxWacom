using UnityEngine;
using System.Collections;
using PressureLib;

/// <summary>
/// Pressure input.
/// 
/// this is a script example of how you could access to wacom tablet datas acquired by the PressureManager script.
/// 
/// </summary>
public class PressureInput : MonoBehaviour {

	/// <summary>
	/// is Pen reversed ?
	/// </summary>
	public bool eraser;
	
	/// <summary>
	/// The cursor position relative to game window
	/// </summary>
	public Vector2 cursorPosition;

	/// <summary>
	/// The standard pen pressure
	/// </summary>
	public float normalPressure;
	
	/// <summary>
	/// The pen tilt. Azimuth and altitude x y.
	/// </summary>
	public Vector2 tilt;
	
	/// <summary>
	/// The wheel on the Intuos and Intuos 2 Airbrush tool are the only tools that report tangential pressure
	/// actually not fully supported.
	/// </summary>
	public float tangentialPressure;
	
	/// <summary>
	/// The pen rotation (orientation) of a wacom "art pen" stylus.
	/// </summary>
	public float rotation; // airbrush stylus's wheel control
	
	void Start(){
		if (PressureManager.instance==null) Debug.LogError("PressureManager not found, please drag the prefab to your hierarchy");
	}
	
	
	void Update () {
		eraser = PressureManager.eraser;
		cursorPosition = PressureManager.cursorPosition;
		normalPressure = PressureManager.normalPressure;
		tilt = PressureManager.tilt;
		tangentialPressure = PressureManager.tangentialPressure;
		rotation = PressureManager.rotation;
	}
	
	void OnGUI(){
		Rect r = new Rect (5,5,140,130);
		GUI.Box (r, "");
		GUI.Label (r, 
			//" tablet running : " + (PressureManager.hasTabletRunning?"ok":"no") + "\n" +
			" mode : " + (PressureManager.eraser?"eraser":"pen") + "\n" +
			" pressure : " + PressureManager.normalPressure + "\n" +
			" tilt hor : " + PressureManager.tilt.x + "\n" + 
			" tilt vert : " + PressureManager.tilt.y + "\n"
		);
	}
}
