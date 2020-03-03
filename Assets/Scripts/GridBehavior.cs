using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehavior : MonoBehaviour
{
    public int rows;
    public int columns;
    public int scale;
    public GameObject gridPrefab;
    public Vector3 bottomLeft = new Vector3(0, 0, 0);
    public GameObject[,] gridArray;
    public int startX = 0;
    public int startY = 0;
    public int endX = 3;
    public int endY = 3;

    private void Awake()
    {
        gridArray = new GameObject[columns, rows];
        GenerateGrid();
    }

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.position = new Vector3(rows / 2, 7.5f, -1.5f);
        Camera.main.transform.rotation = Quaternion.Euler(55, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateGrid()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject obj = Instantiate(gridPrefab, new Vector3(bottomLeft.x + scale * i, bottomLeft.y, bottomLeft.z + scale * j), Quaternion.identity);
                obj.transform.SetParent(gameObject.transform);
                obj.GetComponent<TileInfo>().x = i;
                obj.GetComponent<TileInfo>().y = j;
                gridArray[i, j] = obj;
            }
        }
    }

    void SetUp()
    {
        foreach(GameObject obj in gridArray)
        {
            obj.GetComponent<TileInfo>().visited = -1;
        }
        gridArray[startX, startY].GetComponent<TileInfo>().visited = 0;
    }

    void SetDistance()
    {
        SetUp();
        int x = startX;
        int y = startY;
        int[] testArray = new int[rows * columns];
        for (int step = 0; step < rows * columns; step++)
        {
            foreach (GameObject obj in gridArray)
            {
                if(obj.GetComponent<TileInfo>().visited == step - 1)
                {
                    TestCardinalDirections(obj.GetComponent<TileInfo>().x, obj.GetComponent<TileInfo>().y, step);
                }                    
            }
        }
    }

    bool TestDirection(int x, int y, int step, int dir)
    {
        switch(dir)
        {
            case 1:
                if (y + 1 < rows && gridArray[x, y + 1] && gridArray[x, y + 1].GetComponent<TileInfo>().visited == step)
                    return true;
                else
                    return false;
            case 2:
                if (y - 1 > -1 && gridArray[x, y - 1] && gridArray[x, y - 1].GetComponent<TileInfo>().visited == step)
                    return true;
                else
                    return false;
            case 3:
                if (x + 1 < columns && gridArray[x + 1, y] && gridArray[x + 1, y].GetComponent<TileInfo>().visited == step)
                    return true;
                else
                    return false;
            case 4:
                if (x - 1 > -1 && gridArray[x - 1, y] && gridArray[x - 1, y].GetComponent<TileInfo>().visited == step)
                    return true;
                else
                    return false;
        }
        return false;
    }

    void TestCardinalDirections(int x, int y, int step)
    {
        if (TestDirection(x, y, -1, 1))    
            SetVisited(x, y + 1, step);
        if (TestDirection(x, y, -1, 2))
            SetVisited(x, y - 1, step);
        if (TestDirection(x, y, -1, 3))
            SetVisited(x + 1, y, step);
        if (TestDirection(x, y, -1, 4))
            SetVisited(x - 1, y, step);
    }

    void SetVisited(int x, int y, int step)
    {
        if(gridArray[x, y])
        {
            gridArray[x, y].GetComponent<TileInfo>().visited = step;
        }
    }
}
