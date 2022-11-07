using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardConfiguration : MonoBehaviour
{
    public GameObject board_prefab;

    public string selectedBoardName;
    
    private GameObject plane;

    private string BOARD_ASSETS_FOLDER;
    private Object[] materials;
    private List<string> materialNames;

    void Awake()
    {
        selectedBoardName = "";
        BOARD_ASSETS_FOLDER = "Materials/Boards/";

        materials  = Resources.LoadAll(BOARD_ASSETS_FOLDER, typeof(Material));
        materialNames = new List<string>();
        foreach (Material mat in materials)
        {
            materialNames.Add(mat.name);
        }

        //setBoardPrefabMap("map2"); //   TODO: Isso está aqui como placeholder. Na prática, isso tem que ser chamado pelo componente de UI que seleciona o mapa
    }

    public  Material findMaterialByName(string matName)
    {
        // Provavelmente tem uma forma mais inteligente de fazer isso mas eu n�o sei C#

        foreach(Material mat in materials)
        {
            if(mat.name == matName)
            {
                return mat;
            }
        }

        return null;

    }

    private bool isValidMapName(string mapName)
    {
        foreach (string matName in materialNames)
        {
            Debug.Log("matName: " + matName + ", mapName: " + mapName);
            if (matName == mapName)
            {
                return true;
            }
        }
        return false;
    }

    public void setBoardPrefabMap(string mapName)
    {
        Debug.Log("Setting board prefab");
        if (isValidMapName(mapName))
        {
            selectedBoardName = mapName;
        }
    }

}
