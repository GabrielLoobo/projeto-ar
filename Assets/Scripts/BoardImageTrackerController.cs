using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class BoardImageTrackerController : MonoBehaviour
{
    [SerializeField]
    //private GameObject[] placeblePrefab;

    //private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;

    public GameObject board_prefab;
    //public GameObject accept;
    //public GameObject refuse;

    private GameObject i_board_prefab;
    //private GameObject i_accept;
    //private GameObject i_refuse;

    private bool isBoardPlaced;

    private void Awake()
    {
        isBoardPlaced = false;
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        i_board_prefab = Instantiate(board_prefab, Vector3.zero, Quaternion.identity);
        Debug.Log(trackedImageManager);
        i_board_prefab.SetActive(false);

        trackedImageManager.trackedImagesChanged += ImageChanged; // TODO: Talvez dê merda deixar isso aqui em vez do onEnable, confirmar depois

        //foreach (GameObject prefab in placeblePrefab)
        //{
        //    GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        //    newPrefab.name = prefab.name;
        //    spawnedPrefabs.Add(prefab.name, newPrefab);
        //}
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
        //foreach (ARTrackedImage trackedImage in eventArgs.removed)
        //{
        //    i_board_prefab
        //    //spawnedPrefabs[trackedImage.name].SetActive(false);
        //}
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        Debug.Log("Update Image");
        string name = trackedImage.referenceImage.name;
        Vector3 position = trackedImage.transform.position;
        Quaternion rotation = trackedImage.transform.rotation;



        GameObject prefab = i_board_prefab;
        if (!isBoardPlaced)
        {
            prefab.transform.position = position;
            prefab.transform.rotation = rotation;
            prefab.SetActive(true);
            //isBoardPlaced = true;
        }


        //string name = trackedImage.referenceImage.name;
        //Vector3 position = trackedImage.transform.position;

        //GameObject prefab = spawnedPrefabs[name];
        //prefab.transform.position = position;
        //prefab.SetActive(true);

        //foreach(GameObject go in spawnedPrefabs.Values)
        //{
        //    if(go.name != name)
        //    {
        //        go.SetActive(false);
        //    }
        //}

    }
}
