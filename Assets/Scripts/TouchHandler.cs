using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchHandler : MonoBehaviour
{
    private GameObject characterModel;
    private GameObject UICanvasElements;
    public CharactersImageTrackerController charactersImageTrackerController;

    private float TouchTime;
    // Start is called before the first frame update
    void Start()
    {
        TouchTime = 0;
    }

    
    //public void increaseLifeFromPrefabWrapper(GameObject prefab)
    //{
    //    int newLife = charactersImageTrackerController.spawnedPrefabsLife[prefab.name] + 1;
    //    charactersImageTrackerController.setLifeByName(prefab.name, newLife);
    //}

    //public void decreaseLifeFromPrefabWrapper(GameObject prefab)
    //{
    //    int newLife = charactersImageTrackerController.spawnedPrefabsLife[prefab.name] - 1;
    //    if(newLife < 0)
    //    {
    //        newLife = 0;
    //    }
    //    charactersImageTrackerController.setLifeByName(prefab.name, newLife);
    //}
    // Update is called once per frame
    void Update()
    {
        Touch touch = Input.touches[0];

        if (touch.phase == TouchPhase.Began)
        {
            TouchTime = Time.time;
        }

        if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            if (Time.time - TouchTime <= 0.5)
            {
                //do stuff as a tap​
            }
            else
            {
                // this is a long press or drag​
                Debug.Log(string.Format("Touch count: {0} at {1}", Input.touchCount, Input.GetTouch(0).position));

                Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                RaycastHit raycastHit;
                if (Physics.Raycast(raycast, out raycastHit))
                {
                    characterModel = raycastHit.collider.gameObject;
                    Debug.Log(string.Format("prefab char model child {0}", characterModel.transform.Find("StatusUICanvas")));
                    UICanvasElements = characterModel.transform.Find("StatusUICanvas").transform.GetChild(0).gameObject;

                    Debug.Log(string.Format("UICanvasElements name {0}", UICanvasElements.name));
                    UICanvasElements.SetActive(!UICanvasElements.activeSelf);
                    Debug.Log(string.Format("UICanvasElements enabled {0}", UICanvasElements.activeSelf));

                }
            }

        }
    }
}
