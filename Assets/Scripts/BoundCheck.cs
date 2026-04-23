using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundCheck : MonoBehaviour
{
    [System.Flags] public enum eScreenLocs { onScreen = 0, offRight = 1,
    offLeft = 2, offUp = 4, offDown = 8 }

    public enum eType { center, inset, outset }
    [Header("Inscribed")]
    public eType boundsType = eType.center;
    public float radius = 1f;
    public bool keepOnScreen = true;
    [Header("Dynamic")]
    public eScreenLocs screenLocs;
    public float camWidth, camHeight;
    void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }
    void LateUpdate()
    {
        Vector3 pos = transform.position;
        screenLocs = eScreenLocs.onScreen;
        float checkVal = boundsType == eType.inset ? -radius :
        boundsType == eType.outset ? radius : 0;
        if (pos.x > camWidth + checkVal) {
        pos.x = camWidth + checkVal;
        screenLocs |= eScreenLocs.offRight;
        }
        if (pos.x < -camWidth - checkVal) {
        pos.x = -camWidth - checkVal;
        screenLocs |= eScreenLocs.offLeft;
        }
        if (pos.y > camHeight + checkVal) {
        pos.y = camHeight + checkVal;
        screenLocs |= eScreenLocs.offUp;
        }

        if (pos.y < -camHeight - checkVal) {
        pos.y = -camHeight - checkVal;
        screenLocs |= eScreenLocs.offDown;
        }
        if (keepOnScreen && screenLocs != eScreenLocs.onScreen)
        transform.position = pos;
        }
    public bool isOnScreen {
        get { return screenLocs == eScreenLocs.onScreen; }
    }

    public bool LocIs(eScreenLocs checkloc) {
        if (checkloc == eScreenLocs.onScreen) return isOnScreen;
        return ((screenLocs & checkloc) == checkloc);
    }
    
}
