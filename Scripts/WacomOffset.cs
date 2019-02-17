using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PressureLib;

public class WacomOffset : MonoBehaviour
{

    public LightningArtist latk;
    public SteamVR_NewController mainCtl;
    public SteamVR_NewController altCtl;
    public Renderer wacomGrid;
    public Vector2 wacomCursor = Vector2.zero;
    public bool isActive = false;
    public float triggerDistance = 0.001f;
    public float timeout = 10f;

    private Vector3 origPos;
    private Quaternion origRot;
    private Vector2 lastCursor = Vector2.zero;
    private float lastCursorDist = 0f;
    private float sensitivityX = 2f;
    private float sensitivityY = 2f;
    private bool blockRetrigger = false;
    private float retriggerTimeout = 5f;
    private float timeoutCounter = 0f;

    private void Awake() {
        if (!latk.useCollisions) wacomGrid.enabled = false;

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

        wacomCursorUpdate();
    }

    private void Update() {
        wacomCursorUpdate();

        if (Input.GetMouseButton(0)) timeoutCounter = 0;

        if (Input.GetMouseButtonDown(1)) {
            if (!isActive) {
                wacomModeStart();
            } else {
                wacomModeEnd();
            }
        }

        if (lastCursorDist >= triggerDistance) {
            timeoutCounter = 0f;
            if (!isActive && !blockRetrigger) {
                wacomModeStart();
            }
        } else {
            timeoutCounter += Time.deltaTime;
            if (timeoutCounter > timeout) wacomModeEnd();
        }

        if (isActive) {
            wacomModeUpdate();
        }
    }

    void wacomCursorUpdate() {
        lastCursor = new Vector2(wacomCursor.x, wacomCursor.y);
        wacomCursor = new Vector2(PressureManager.cursorPosition.x / Screen.width, (PressureManager.cursorPosition.y / Screen.height) - 0.5f);
        lastCursorDist = Vector2.Distance(wacomCursor, lastCursor);
    }

    void wacomModeStart() {
        latk.useCollisions = false;
        timeoutCounter = 0f;
        isActive = true;
        wacomGrid.enabled = true;
        transform.parent = altCtl.transform;
        transform.localPosition = origPos;
        transform.localRotation = origRot;
    }

    void wacomModeEnd() {
        timeoutCounter = 0f;
        isActive = false;
        if (!latk.useCollisions) wacomGrid.enabled = false;
        transform.parent = mainCtl.transform;
        transform.localPosition = origPos;
        transform.localRotation = origRot;
        StartCoroutine(startTimeout());
    }

    void wacomModeUpdate() {
        transform.localPosition = new Vector3(wacomCursor.x, transform.localPosition.y, wacomCursor.y);
        mainCtl.triggerPressed = Input.GetMouseButton(0);
    }

    private IEnumerator startTimeout() {
        blockRetrigger = true;
        yield return new WaitForSeconds(retriggerTimeout);
        blockRetrigger = false;
    }

}
 