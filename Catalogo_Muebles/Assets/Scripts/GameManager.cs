using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GameManager : MonoBehaviour
{
    public event Action OnMainMenu;
    public event Action OnItemsMenu;
    public event Action OnARPosition;

    [SerializeField] public ARPointCloudManager pointCloud;

    public static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {        
        MainMenu();
    }

    public void MainMenu()
    {
        OnMainMenu?.Invoke();
        pointCloud.enabled = false;
        Debug.Log("Main Menu Activated");
    }

    public void ItemsMenu()
    {
        OnItemsMenu?.Invoke();
        Debug.Log("Items Menu Activated");
    }

    public void ARPosition()
    {
        OnARPosition?.Invoke();
        Debug.Log("AR Position Activated");
    }

    public void CloseApp()
    {
        Application.Quit();
    }
}
