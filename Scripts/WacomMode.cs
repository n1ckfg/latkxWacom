using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WacomMode : MonoBehaviour {

    private Transform origParent;
    private Vector3 origPos;
    private Vector3 origScale;
    private Quaternion origRot;
    private bool firstRun;

    private void Start() {
        origParent = transform.parent;
        saveTransform();
    }

    private void saveTransform() {
        origPos = transform.localPosition;
        origRot = transform.localRotation;
        origScale = transform.localScale;
    }

    private void restoreTransform() {
        transform.localPosition = origPos;
        transform.localRotation = origRot;
        transform.localScale = origScale;
    }

    public void attach() {
        transform.SetParent(origParent);
        restoreTransform();
    }

    public void detach() {
        transform.SetParent(null, true);
    }

}
