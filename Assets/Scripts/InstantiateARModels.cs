using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class InstantiateARModels : MonoBehaviour
{
    public GameObject instantiatePrefab;
    private GameObject spawnPrefab;
    private Vector2 touchPosition;
    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> arRaycasthitList = new List<ARRaycastHit>();

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        if (!TouchInput(touchPosition))
            return;
        if (arRaycastManager.Raycast(touchPosition, arRaycasthitList, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = arRaycasthitList[0].pose;
            if (spawnPrefab == null)
                spawnPrefab = Instantiate(instantiatePrefab, hitPose.position, hitPose.rotation);
            else
                spawnPrefab.transform.position = hitPose.position;
        }
        TouchInput(touchPosition);
    }

    private bool TouchInput(Vector2 _touchPos)
    {
        if (Input.touchCount == 1)
        {
            _touchPos = Input.GetTouch(0).position;
            return true;
        }
        _touchPos = default;
        return false;
    }
}
