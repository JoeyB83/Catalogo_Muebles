using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARInteractionManager : MonoBehaviour
{
    [SerializeField] Camera arCamera;
    ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    
    GameObject item3DModel;
    public GameObject instObject;
    public Vector3 objPos;
    public Quaternion objRot;

    public GameObject Item3DModel
    {
        set
        {
            item3DModel = value;
        }
        get
        {
            return item3DModel;
        }        
    }

    // Start is called before the first frame update
    void Start()
    {        
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount < 1) return;
        if (Input.GetTouch(0).phase != TouchPhase.Began) return;
        if (instObject != null) return;
        LaunchRaycast(Input.GetTouch(0).position);        
    }   

    public void DeleteItem()
    {
        Destroy(instObject);       
        GameManager.instance.ARPosition();        
    }

    public void DeleteAllItem()
    {
        Destroy(instObject);        
        GameManager.instance.MainMenu();
    }
    public void BackToMain()
    {        
        GameManager.instance.ItemsMenu();
    }

    void LaunchRaycast(Vector2 raycastPoint)
    {
        if (aRRaycastManager.Raycast(raycastPoint, hits, TrackableType.PlaneWithinPolygon))
        {
            foreach (ARRaycastHit rh in hits)
            {
                Pose pose = rh.pose;

                Vector3 dir = (Camera.main.transform.position - pose.position).normalized;

                Vector3 dirCorrected = new Vector3(dir.x, 0f, dir.z);

                if (instObject == null)
                {
                    objPos = pose.position;

                    objRot = pose.rotation;

                    instObject = Instantiate(item3DModel, objPos, objRot);

                    instObject.transform.rotation = Quaternion.LookRotation(dirCorrected);
                }                              
            }

        }
    }


}
