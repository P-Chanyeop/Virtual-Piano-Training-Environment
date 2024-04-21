using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlaceOnPlane : MonoBehaviour
{
    public ARRaycastManager arRaycaster;
    public GameObject placeObject;

    GameObject spawnObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // UpdateCenterObject();   

    }

    private void PlaceObjectByTouch()
    {
        // 터치가 하나라도 일어났을 때
        if (Input.touchCount > 0)
        {
            // 0번째 터치정보를 저장. 즉 가장 첫 번째 터치만 저장.
            Touch touch = Input.GetTouch(0);

            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            
            // 터치한곳이 평면이라면
            if (arRaycaster.Raycast(touch.position, hits, TrackableType.Planes))
            {
                // 터치 정보의 pose 에
                Pose hitPose = hits[0].pose;

                // 오브젝트가 없을 경우는 새로 생성
                if (!spawnObject)
                {
                    spawnObject = Instantiate(placeObject, hitPose.position, hitPose.rotation);
                }
                // 이미 있을 경우 위치를 변경
                else
                {
                    spawnObject.transform.position = hitPose.position;
                    spawnObject.transform.rotation = hitPose.rotation;
                }
            }
        }
    }

    private void UpdateCenterObject()
    {
        Vector3 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        arRaycaster.Raycast(screenCenter, hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            Pose placementPose = hits[0].pose;
            placeObject.SetActive(true);
            placeObject.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            
        }
        else
        {
            placeObject.SetActive(false);
        }
    }
}
