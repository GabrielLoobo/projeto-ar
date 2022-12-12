using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

[RequireComponent(typeof(ARTrackedImageManager))]
public class BoardImageTrackerController : MonoBehaviour
{
    public BoardConfiguration boardConfiguration;
    private ARTrackedImageManager trackedImageManager;

    public GameObject board_prefab;
    private Button accept;

    private GameObject i_board_prefab;

    private bool isBoardPlaced;


    private void onConfirmClick()
    {
        isBoardPlaced = true;
        accept.onClick.RemoveAllListeners();
        accept.gameObject.SetActive(false);
        boardConfiguration.setBoardPrefabMap("map1");
    }

    private void Awake()
    {
        isBoardPlaced = false;
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        i_board_prefab = Instantiate(board_prefab, Vector3.zero, Quaternion.identity);
        i_board_prefab.SetActive(false);

        accept = GameObject.Find("Confirm-UI").GetComponent<Button>();
        accept.onClick.AddListener(onConfirmClick);

        trackedImageManager.trackedImagesChanged += ImageChanged; // TODO: Talvez dê merda deixar isso aqui em vez do onEnable, confirmar depois
    }

    void onEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    void onDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }


    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        Debug.Log("Image changed");
        Debug.Log(eventArgs);
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        if (!isBoardPlaced)
        {
            Debug.Log("Update Image");
            string name = trackedImage.referenceImage.name;
            Vector3 position = trackedImage.transform.position;
            Quaternion rotation = trackedImage.transform.rotation;


            GameObject prefab = i_board_prefab;

            i_board_prefab.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = boardConfiguration.findMaterialByName(boardConfiguration.selectedBoardName);
            prefab.transform.position = position;
            prefab.transform.rotation = rotation;
            prefab.SetActive(true);
        }

    }
}
