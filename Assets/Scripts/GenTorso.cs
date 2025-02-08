using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

public class GenTorso : MonoBehaviour
{
    List<List<List<bool>>> genTorsoList = new List<List<List<bool>>>
    {
        new List<List<bool>>  // 0
        {
            new List<bool> { false, true, true, true, true, false }, 
            new List<bool> { true, true, true, true, true, true },
            new List<bool> { true, true, true, true, true, true  },
            new List<bool> { true, true, true, true, true, true  },
            new List<bool> { true, true, true, true, true, true  },
            new List<bool> { false, true, true, true, true, false }
        },
        new List<List<bool>>  // 1
        {
            new List<bool> { true, true, true, true, true, true },
            new List<bool> { true, false, false, false, false, true },
            new List<bool> { true, false, false, false, false, true },
            new List<bool> { true, false, false, false, false, true },
            new List<bool> { true, false, false, false, false, true },
            new List<bool> { true, true, true, true, true, true },
        },
        new List<List<bool>>  // 2
        {
            new List<bool> { true, true, true, true, true, true },
            new List<bool> { true, false, false, false, false, true },
            new List<bool> { true, false, false, false, false, true },
            new List<bool> { true, false, false, false, false, true },
            new List<bool> { true, false, false, false, false, true },
            new List<bool> { true, true, true, true, true, true },
        },
        new List<List<bool>>  // 3
        {
            new List<bool> { true, true, true, true, true, true },
            new List<bool> { true, false, false, false, false, true },
            new List<bool> { true, false, false, false, false, true },
            new List<bool> { true, false, false, false, false, true },
            new List<bool> { true, false, false, false, false, true },
            new List<bool> { true, true, true, true, true, true },
        },
        new List<List<bool>>  // 4
        {
            new List<bool> { true, true, true, true, true, true },
            new List<bool> { true, false, false, false, false, true },
            new List<bool> { true, false, false, false, false, true },
            new List<bool> { true, false, false, false, false, true },
            new List<bool> { true, false, false, false, false, true },
            new List<bool> { true, true, true, true, true, true },
        },
        new List<List<bool>>  // 5
        {
            new List<bool> { false, true, true, true, true, false }, 
            new List<bool> { true, true, true, true, true, true },
            new List<bool> { true, true, true, true, true, true  },
            new List<bool> { true, true, true, true, true, true  },
            new List<bool> { true, true, true, true, true, true  },
            new List<bool> { false, true, true, true, true, false }
        }
    };

    public float cubeDimensions = 0.25f;

    void Start()
    {
       GenerateListObject(genTorsoList);
    }


    void Update()
    {
        
    }

    public GameObject GenerateCube(float cubeDimm)
    {
        GameObject gendCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        gendCube.transform.localScale =  new Vector3(cubeDimm,cubeDimm,cubeDimm);
        gendCube.name = "gCube";
        return gendCube;
    }

    GameObject GenerateListObject(List<List<List<bool>>> objectList)
    {
        GameObject gendObject = new GameObject("gObject");
        for (int x = 0; x < objectList.Count; x++)
        {
            GameObject layerObject = new GameObject("gLayer "+ x.ToString());
            for (int y = 0; y < objectList[x].Count; y++)
            {
                for (int z = 0; z < objectList[x][y].Count; z++)
                {
                    if (objectList[x][y][z]) // Only create a cube if true
                    {
                        GameObject gCube= GenerateCube(cubeDimensions);
                        gCube.transform.position = new Vector3(y*cubeDimensions, x*cubeDimensions, z*cubeDimensions);
                        gCube.transform.parent = layerObject.transform; // Attach to parent
                    }
                }
            }
            layerObject.transform.parent = gendObject.transform; // Attach to parent
        }

        return gendObject;
    }

    public void printListItems(List<bool> listItems)
    {
        string allItems = "";
        foreach (bool item in listItems)
        {
            allItems += item.ToString() + ", ";
        }
        Debug.Log(allItems);
    }
}
