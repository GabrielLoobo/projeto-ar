using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelect : MonoBehaviour
{
    public BoardConfiguration boardConfiguration;
    public Canvas mapSelectCanvas;
    public string mapName;

    public void onClickSelectMap()
    {
        boardConfiguration.setBoardPrefabMap(mapName);
        nextStep();
    }

    public void nextStep()
    {
        if (boardConfiguration.selectedBoardName != "")
        {
            mapSelectCanvas.enabled = false;
        }
    }
}
