using UnityEngine;
using UnityEditor;
using System.Collections;
using PressureLib;

[CustomEditor (typeof(PressureManager))]
public class PressureManagerEditor : Editor {
	
	public override void OnInspectorGUI()
    {
		EditorUtility.SetDirty( target );
		GUILayout.BeginHorizontal();
		GUILayout.Label("--Datas updated only in game mode--");
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		GUILayout.Label("Cursor pos : " + PressureManager.cursorPosition);
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		GUILayout.Label("Mode : " + (PressureManager.eraser?"Eraser":"Pen"));
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		GUILayout.Label("Pressure : " + PressureManager.normalPressure);
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		GUILayout.Label("Tilt : " + PressureManager.tilt);
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		GUILayout.Label("Tangent Pressure : " + PressureManager.tangentialPressure);
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		GUILayout.Label("Rotation : " + PressureManager.rotation);
		GUILayout.EndHorizontal();
    }
	
	// Use this for initialization
	void Start () {
			//EditorUtility.SetDirty( target );

	}
	
	//void OnGUI()
	
	// Update is called once per frame
//	void OnGUI () {
//		GUILayout.BeginHorizontal();
//		GUILayout.Label("Cursor pos : " + PressureManager.cursorPosition + "\n");
//		GUILayout.EndHorizontal();
//		GUILayout.BeginHorizontal();
//		GUILayout.Label("Mode : " + (PressureManager.eraser?"Eraser":"Pen") + "\n");
//		GUILayout.EndHorizontal();
//		GUILayout.BeginHorizontal();
//		GUILayout.Label("Pressure : " + PressureManager.normalPressure + "\n");
//		GUILayout.EndHorizontal();
//		GUILayout.BeginHorizontal();
//		GUILayout.Label("Tilt : " + PressureManager.tilt + "\n");
//		GUILayout.EndHorizontal();
//		GUILayout.BeginHorizontal();
//		GUILayout.Label("Tangent Pressure : " + PressureManager.tangentialPressure + "\n");
//		GUILayout.EndHorizontal();
//		GUILayout.BeginHorizontal();
//		GUILayout.Label("Rotation : " + PressureManager.rotation + "\n");
//		GUILayout.EndHorizontal();
//		
//					EditorGUILayout.LabelField ("Mouse Position: ", Event.current.mousePosition.ToString ());
//			
//			// Repaint the window as wantsMouseMove doesnt trigger a repaint automatically
//			if (Event.current.type == EventType.MouseMove){
//				Repaint ();
//		Debug.Log ("efzef");
//		}
//	if (GUI.changed) {  EditorUtility.SetDirty (target); } 	
//	}
//	
	



}
