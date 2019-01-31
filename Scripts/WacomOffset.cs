using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PressureLib;

public class WacomOffset : MonoBehaviour
{

    public SteamVR_NewController ctl;
    public Vector2 wacomCursor = Vector2.zero;
    public bool isActive = false;
    public float triggerDistance = 0.0001f;
    public float timeout = 2f;

    private Vector3 origPos;
    private Vector2 lastCursor;
    private float sensitivityX = 2f;
    private float sensitivityY = 2f;

    private void Awake() {
        // for some reason sensitivity is greater in build than in editor
#if !UNITY_EDITOR
		float sensReduce = 8f;
		sensitivityX /= sensReduce;
		sensitivityY /= sensReduce;
#endif
    }

    private void Start() {
        origPos = transform.localPosition;
    }

    private void Update() {
        wacomCursor = new Vector2((PressureManager.cursorPosition.x / Screen.width) - 0.5f, (PressureManager.cursorPosition.y / Screen.height) - 0.5f);

        if (Vector2.Distance(wacomCursor, lastCursor) >= triggerDistance) {
            isActive = true;
            transform.localPosition = new Vector3(wacomCursor.x, transform.localPosition.y, wacomCursor.y);
        } else {
            StartCoroutine(startTimeout());
        }

        if (isActive) {
            ctl.triggerPressed = Input.GetMouseButton(0);
        } else {
            transform.localPosition = origPos;
        }

        lastCursor = new Vector2(wacomCursor.x, wacomCursor.y);
    }

    private IEnumerator startTimeout() {
        yield return new WaitForSeconds(timeout);
        if (Vector2.Distance(wacomCursor, lastCursor) < triggerDistance) {
            isActive = false;
        }
    }

}
