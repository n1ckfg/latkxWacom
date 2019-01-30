using UnityEngine;
using System.Collections;

/// WacomLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the WacomLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a WacomLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
public class WacomLook : MonoBehaviour {

	public PressureInput pressureInput;
	public Vector2 wacomCursor = Vector2.zero;
		
	public enum RotationAxes { WacomXAndY = 0, WacomX = 1, WacomY = 2 }
	public RotationAxes axes = RotationAxes.WacomXAndY;
	public float sensitivityX = 2F;
	public float sensitivityY = 2F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationY = 0F;

	void Awake() {
		if (pressureInput == null) pressureInput = GetComponent<PressureInput>();

		// for some reason sensitivity is greater in build than in editor
		#if !UNITY_EDITOR
		float sensReduce = 8f;
		sensitivityX /= sensReduce;
		sensitivityY /= sensReduce;
		#endif
	}

	void Start() {
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	}

	void Update() {
		if (Input.GetMouseButton(1)) {
			wacomCursor = new Vector2((pressureInput.cursorPosition.x / Screen.width) - 0.5f, (pressureInput.cursorPosition.y / Screen.height) - 0.5f);

			if (axes == RotationAxes.WacomXAndY) {
				float rotationX = transform.localEulerAngles.y + wacomCursor.x * sensitivityX;

				rotationY += wacomCursor.y * sensitivityY;
				rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

				transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
			} else if (axes == RotationAxes.WacomX) {
				transform.Rotate(0, wacomCursor.x * sensitivityX, 0);
			} else {
				rotationY += wacomCursor.y * sensitivityY;
				rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

				transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
			}
		}
	}

}