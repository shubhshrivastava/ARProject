using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class InstantiateARModels : MonoBehaviour
{
    [Header("Intantiate3D-Model")]
    public GameObject instantiatePrefab;
    private GameObject spawnPrefab;
    public GameObject testTransform;
    private Vector2 touchPosition;
    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> arRaycasthitList = new List<ARRaycastHit>();

    [Header("Scaling")]
    private float initialFingerDistance;
    private Vector3 initialScale;
    private Vector3 finalScale;

    [Header("Rotation")]
    private Touch touchConti;
    private float getrotatioDir;

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        if (!TouchInput(out touchPosition, touchConti))
            return;
        if (arRaycastManager.Raycast(touchPosition, arRaycasthitList, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = arRaycasthitList[0].pose;
            if (spawnPrefab == null)
                spawnPrefab = Instantiate(instantiatePrefab, hitPose.position, hitPose.rotation);
            else
                spawnPrefab.transform.position = hitPose.position;
        }
    }

    private bool TouchInput(out Vector2 _touchPos, Touch _touchcontinue)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (Input.touchCount == 1 && touch.phase == TouchPhase.Ended)
            {
                Debug.Log("Tap");
                _touchPos = Input.GetTouch(0).position;
                return true;
            }
            if (Input.touchCount == 2)
                Scale(touch);
            if (Input.touchCount == 1)
                Rotate(touch);
        }
        _touchPos = default;
        return false;
    }

    private void Rotate(Touch touch)
    {
        Debug.Log("Rotation");
        if (touch.phase == TouchPhase.Moved)
        {
            Debug.Log("Moved");
            getrotatioDir = touch.deltaPosition.y;
            Debug.Log("calculateRotationDirection" + getrotatioDir);
            float newgetRotationDirSpeed = getrotatioDir;
            Debug.Log("newgetRotationDir" + newgetRotationDirSpeed);

#if UNITY_EDITOR
            testTransform.transform.Rotate(Vector3.up * newgetRotationDirSpeed);
#endif
#if !UNITY_EDITOR && UNITY_ANDROID
                    spawnPrefab.transform.Rotate(Vector3.up * newgetRotationDirSpeed);
#endif
        }
    }

    private void Scale(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
        {
            Debug.Log("touchPhase has been started");
            initialFingerDistance = Vector3.Distance(Input.touches[0].position, Input.touches[1].position);
            Debug.Log("InitalfigerDistance:" + initialFingerDistance);
#if !UNITY_EDITOR && UNITY_ANDROID
                    initialScale = spawnPrefab.transform.localScale;
#endif
#if UNITY_EDITOR
            initialScale = testTransform.transform.localScale;
#endif
            Debug.Log("initialScale:" + initialScale);
        }
        if (touch.phase == TouchPhase.Moved)
        {
            Debug.Log("Moved");
            float currentFingerDistance = Vector3.Distance(Input.touches[0].position, Input.touches[1].position);
            Debug.Log("CurrentfingerDistance" + currentFingerDistance);
            float calculateDistance = currentFingerDistance / initialFingerDistance;
            Debug.Log("CalculateDistance" + calculateDistance);
#if !UNITY_EDITOR && UNITY_ANDROID
                    spawnPrefab.transform.localScale = initialScale * calculateDistance;
#endif
#if UNITY_EDITOR
            testTransform.transform.localScale = initialScale * calculateDistance;
            Debug.Log("FinalScale" + testTransform.transform.localScale);
#endif
        }

    }
}