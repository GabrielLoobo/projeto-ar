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
    public Dictionary<string, int> spawnedPrefabsLife = new Dictionary<string, int>();
    public int maxLife;
    private ARTrackedImageManager trackedImageManager;
    private GameObject characterUIElements;
    private GameObject prefabModel;

    private void Awake()
    {
        maxLife = 20;
        trackedImageManager = GetComponent<ARTrackedImageManager>();

        trackedImageManager.trackedImagesChanged += ImageChanged;

        foreach (GameObject prefab in placeblePrefab)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.SetActive(false);
            newPrefab.name = prefab.name;
            characterUIElements = newPrefab.transform.GetChild(0).transform.Find("StatusUICanvas").transform.GetChild(0).gameObject;
            characterUIElements.SetActive(false);
            Debug.Log(string.Format("prefab charUI: {0}", characterUIElements));
            
            spawnedPrefabsLife.Add(prefab.name, maxLife);
            characterUIElements.transform.Find("maxlife-text").GetComponent<Text>().text = string.Format("{0}", maxLife);
            characterUIElements.transform.Find("currentlife-text").GetComponent<Text>().text = string.Format("{0}", maxLife);
            spawnedPrefabs.Add(prefab.name, newPrefab);


            void _dec()
            {
                decreaseLifeFromPrefabWrapper(newPrefab);
            }

            void _inc()
            {
                increaseLifeFromPrefabWrapper(newPrefab);
            }

            characterUIElements.transform.Find("sub-button").GetComponent<Button>().onClick.RemoveAllListeners();
            characterUIElements.transform.Find("sub-button").GetComponent<Button>().onClick.AddListener(_dec);
            characterUIElements.transform.Find("add-button").GetComponent<Button>().onClick.RemoveAllListeners();
            characterUIElements.transform.Find("add-button").GetComponent<Button>().onClick.AddListener(_inc);
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

    public void setLifeByName(string name, int life) {
        spawnedPrefabsLife[name] = life;
    }

    public void increaseLifeFromPrefabWrapper(GameObject prefab)
    {
        int newLife = spawnedPrefabsLife[prefab.name] + 1;
        setLifeByName(prefab.name, newLife);
        Debug.Log(string.Format("Current prefab life: {0}", spawnedPrefabsLife[prefab.name]));
        //characterUIElements.transform.Find("currentlife-text").GetComponent<Text>().text = string.Format("{0}", spawnedPrefabsLife[prefab.name]);
    }

    public void decreaseLifeFromPrefabWrapper(GameObject prefab)
    {
        int newLife = spawnedPrefabsLife[prefab.name] - 1;
        if (newLife < 0)
        {
            newLife = 0;
        }
        setLifeByName(prefab.name, newLife);
        Debug.Log(string.Format("Current prefab life: {0}", spawnedPrefabsLife[prefab.name]));
        //characterUIElements.transform.Find("currentlife-text").GetComponent<Text>().text = string.Format("{0}", spawnedPrefabsLife[prefab.name]);
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

            prefabModel = prefab.transform.GetChild(0).gameObject;
            characterUIElements = prefab.transform.GetChild(0).transform.Find("StatusUICanvas").transform.GetChild(0).gameObject;

            // TODO: Ajustar esse offset para aparecerem acima da cabeça do personagem
            //Vector3 characterHeightOffset = Camera.main.WorldToScreenPoint(new Vector3(0, prefabModel.transform.localScale.y/2, 0));
            Vector3 modelPosOnCam = Camera.main.WorldToScreenPoint(prefabModel.transform.position);
            characterUIElements.transform.position = modelPosOnCam;

            

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
