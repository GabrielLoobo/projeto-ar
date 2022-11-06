using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class CharactersImageTrackerController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeblePrefab;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;



    private void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();

        trackedImageManager.trackedImagesChanged += ImageChanged; // TODO: Talvez dê merda deixar isso aqui em vez do onEnable, confirmar depois

        foreach (GameObject prefab in placeblePrefab)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.SetActive(false);
            newPrefab.name = prefab.name;
            spawnedPrefabs.Add(prefab.name, newPrefab);
        }
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
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawnedPrefabs[trackedImage.name].SetActive(false);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {   
 
        string name = trackedImage.referenceImage.name;

        if (name != "board")
        {
            Vector3 position = trackedImage.transform.position;
            Quaternion rotation = trackedImage.transform.rotation;

            GameObject prefab = spawnedPrefabs[name];
            prefab.transform.position = position;
            prefab.transform.rotation = rotation;
            prefab.SetActive(true);

            foreach (GameObject go in spawnedPrefabs.Values)
            {
                if (go.name != name)
                {
                    go.SetActive(false);
                }
            }
        }

    }
}
