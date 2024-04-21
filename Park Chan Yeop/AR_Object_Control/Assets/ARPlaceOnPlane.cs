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
        // ��ġ�� �ϳ��� �Ͼ�� ��
        if (Input.touchCount > 0)
        {
            // 0��° ��ġ������ ����. �� ���� ù ��° ��ġ�� ����.
            Touch touch = Input.GetTouch(0);

            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            
            // ��ġ�Ѱ��� ����̶��
            if (arRaycaster.Raycast(touch.position, hits, TrackableType.Planes))
            {
                // ��ġ ������ pose ��
                Pose hitPose = hits[0].pose;

                // ������Ʈ�� ���� ���� ���� ����
                if (!spawnObject)
                {
                    spawnObject = Instantiate(placeObject, hitPose.position, hitPose.rotation);
                }
                // �̹� ���� ��� ��ġ�� ����
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
