using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class BoardImageTracker : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeblePrefab;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;

    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        Debug.Log(trackedImageManager);
        Debug.Log("here");
        print("aquii");
        foreach (GameObject prefab in placeblePrefab)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            spawnedPrefabs.Add(prefab.name, newPrefab);
        }
    }
    
    private void onEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void onDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        Debug.Log(eventArgs);

        foreach(ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }
        //foreach (ARTrackedImage trackedImage in eventArgs.removed)
        //{
        //    UpdateImage(trackedImage);
        //}
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        Debug.Log("here");
        Debug.Log(trackedImage);
    }
}
