using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ItemButtonManager : MonoBehaviour
{
    string itemName;
    string itemDescription;
    Sprite itemImage;
    GameObject item3DModel;
    ARInteractionManager interactionManager;
    GameObject miniModel;
    

    public string ItemName { 
        set 
        { 
            itemName = value; 
        } 
    }    

    public string ItemDescription { set => itemDescription = value; }

    public Sprite ItemImage { set => itemImage = value; }

    public GameObject Item3DModel { set => item3DModel = value; }

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = itemName;
        transform.GetChild(2).GetComponent<RawImage>().texture = itemImage.texture;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemDescription;
        miniModel = GameObject.Find("Delete");
        

        var button = GetComponent<Button>();
        button.onClick.AddListener(GameManager.instance.ARPosition);
        button.onClick.AddListener(Create3DModel);
        button.onClick.AddListener(CreateMiniModel);

        interactionManager = FindObjectOfType<ARInteractionManager>();
    }

    private void CreateMiniModel()
    {
        miniModel.transform.GetChild(0).GetComponent<RawImage>().texture = itemImage.texture;
    }

    private void Create3DModel()
    {
        if (interactionManager.instObject == null)
        {
            interactionManager.Item3DModel = item3DModel;
            GameManager.instance.pointCloud.enabled = true;
        }
        else
        {
            Destroy(interactionManager.instObject);
            interactionManager.Item3DModel = Instantiate(item3DModel, interactionManager.objPos, interactionManager.objRot);
            interactionManager.instObject = interactionManager.Item3DModel;
        }

    }  


}
