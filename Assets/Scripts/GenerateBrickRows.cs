using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateBrickRows: MonoBehaviour
{
    public GameObject referencePoint;
    public List<GameObject> prefabList;

    [Range(0, 10)] public int rows;
    [Range(0, 11)] public int rowSize;

    public float verticalPadding;
    public float horizontalPadding;

    private Vector3 _brickScale;

    private void Awake() {
        _brickScale = prefabList[0].transform.localScale;
    }

    private void Start()
    {
        DrawBrickRaws();
    }

    private void DrawBrickRaws()
    {
        GameObject instance = null;
        Vector3 instancePos;
            
        int prefabType = 0;
        float incrementX = 0f;
        float incrementY = 0f;
        
        for (int iRow = 0; iRow < rows; iRow++)
        {
            prefabType = Random.Range(0, prefabList.Count);
            
            for (int iCol = 0; iCol < rowSize; iCol++)
            {
                instance = Instantiate(prefabList[prefabType], referencePoint.transform);
                
                instancePos = instance.transform.position;
                instancePos.x += incrementX;
                instancePos.y += incrementY;
                instance.transform.position = instancePos;
                incrementX += horizontalPadding + _brickScale.x;
            }

            incrementX = 0f;
            incrementY += verticalPadding + _brickScale.y;
        }
    }
}
