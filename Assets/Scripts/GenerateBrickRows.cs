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
        Vector3 nextPosition = referencePoint.transform.position;
        int prefabType = 0;
        
        for (int iRow = 0; iRow < rows; iRow++)
        {
            prefabType = Random.Range(0, prefabList.Count);
            
            for (int iCol = 0; iCol < rowSize; iCol++)
            {
                Instantiate(prefabList[prefabType], nextPosition, Quaternion.identity);
                nextPosition.x += _brickScale.x + 0.5f;
                Debug.Log("x-pos: " + nextPosition.x);
            }

            nextPosition.x = referencePoint.transform.position.x;
            nextPosition.y -= _brickScale.y + 0.5f;
        }
    }
}
