#pragma strict
	/// <summary>
	/// is Pen reversed ?
	/// </summary>
	var eraser : boolean;
	
	/// <summary>
	/// The cursor position relative to game window
	/// </summary>
	var cursorPosition : Vector2;

	/// <summary>
	/// The standard pen pressure
	/// </summary>
	var normalPressure : float;
	
	/// <summary>
	/// The pen tilt. Azimuth and altitude x y.
	/// </summary>
	var tilt : Vector2;
	
	/// <summary>
	/// The wheel on the Intuos and Intuos 2 Airbrush tool are the only tools that report tangential pressure
	/// actually not fully supported.
	/// </summary>
	var tangentialPressure : float;
	
	/// <summary>
	/// The pen rotation (orientation) of a wacom "art pen" stylus.
	/// </summary>
	var rotation : float;
	
	
	function Start(){
		if (PressureLib.PressureManager.instance==null) Debug.LogError("PressureManager not found, please drag the prefab to your hierarchy");
	}
	
	
	function Update () {
		eraser = PressureLib.PressureManager.eraser;
		cursorPosition = PressureLib.PressureManager.cursorPosition;
		normalPressure = PressureLib.PressureManager.normalPressure;
		tilt = PressureLib.PressureManager.tilt;
		tangentialPressure = PressureLib.PressureManager.tangentialPressure;
		rotation = PressureLib.PressureManager.rotation;
	}