using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PressureLib;

public class WacomOffset : MonoBehaviour
{

    public SteamVR_NewController mainCtl;
    public SteamVR_NewController altCtl;
    public Renderer wacomGrid;
    public Vector2 wacomCursor = Vector2.zero;
    public bool isActive = false;
    public float triggerDistance = 0.0001f;
    public float timeout = 2f;

    private Vector3 origPos;
    private Quaternion origRot;
    private Vector2 lastCursor = Vector2.zero;
    private float sensitivityX = 2f;
    private float sensitivityY = 2f;

    private void Awake() {
        wacomGrid.enabled = false;

        // for some reason sensitivity is greater in build than in editor
#if !UNITY_EDITOR
		float sensReduce = 8f;
		sensitivityX /= sensReduce;
		sensitivityY /= sensReduce;
#endif
    }

    private void Start() {
        origPos = transform.localPosition;
        origRot = transform.localRotation;
        //StartCoroutine(startTimeout());
    }

    private void Update() {
        wacomCursor = new Vector2(PressureManager.cursorPosition.x / Screen.width, (PressureManager.cursorPosition.y / Screen.height) - 0.5f);

        //if (Time.realtimeSinceStartup > timeout) {
            if (Input.GetMouseButtonDown(1)) {
                if (!isActive) {
                    isActive = true;
                    wacomGrid.enabled = true;
                    transform.parent = altCtl.transform;
                    transform.localPosition = origPos;
                    transform.localRotation = origRot;
                } else {
                    isActive = false;
                    wacomGrid.enabled = false;
                    transform.parent = mainCtl.transform;
                    transform.localPosition = origPos;
                    transform.localRotation = origRot;
                }
            }

            if (isActive) {
                wacomGrid.enabled = true;
                transform.localPosition = new Vector3(wacomCursor.x, transform.localPosition.y, wacomCursor.y);
                mainCtl.triggerPressed = Input.GetMouseButton(0);
            }
        //}

        //lastCursor = new Vector2(wacomCursor.x, wacomCursor.y);
    }

    /*
    private IEnumerator startTimeout() {
        while (true) {
            yield return new WaitForSeconds(timeout);
            if (Vector2.Distance(wacomCursor, lastCursor) >= triggerDistance) isActive = true;
        }
    }
    */

}
 