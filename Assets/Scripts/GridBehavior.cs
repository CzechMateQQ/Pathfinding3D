using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehavior : MonoBehaviour
{
    public int rows;
    public int columns;
    public int scale;
    public int startX = 0;
    public int startY = 0;
    public int endX = 3;
    public int endY = 3;

    private bool targetFound = false;

    public GameObject gridPrefab;
    public GameObject[,] gridArray;

    public Vector3 bottomLeft = new Vector3(0, 0, 0);

    public List<GameObject> open;
    public List<GameObject> closed;

    private void Awake()
    {
        gridArray = new GameObject[columns, rows];
        GenerateGrid();
        SetUp();
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
        //while(!targetFound)
        //{

        //}
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
        gridArray[startX, startY].GetComponent<TileInfo>().fScore = 0;
        open.Add(gridArray[startX, startY]);
    }

    //void SetDistance()
    //{
    //    SetUp();
    //    int x = startX;
    //    int y = startY;
    //    int[] testArray = new int[rows * columns];
    //    for (int step = 0; step < rows * columns; step++)
    //    {
    //        foreach (GameObject obj in gridArray)
    //        {
    //            if(obj.GetComponent<TileInfo>().visited == step - 1)
    //            {
    //                TestCardinalDirections(obj.GetComponent<TileInfo>().x, obj.GetComponent<TileInfo>().y);
    //            }                    
    //        }
    //    }
    //}

    bool TestDirection(int x, int y, int dir)
    {
        switch(dir)
        {
            case 1:
                if (y + 1 < rows && gridArray[x, y + 1] && gridArray[x, y + 1].GetComponent<TileInfo>().visited < 1)
                    return true;
                else
                    return false;
            case 2:
                if (y - 1 > -1 && gridArray[x, y - 1] && gridArray[x, y - 1].GetComponent<TileInfo>().visited < 1)
                    return true;
                else
                    return false;
            case 3:
                if (x + 1 < columns && gridArray[x + 1, y] && gridArray[x + 1, y].GetComponent<TileInfo>().visited < 1)
                    return true;
                else
                    return false;
            case 4:
                if (x - 1 > -1 && gridArray[x - 1, y] && gridArray[x - 1, y].GetComponent<TileInfo>().visited < 1)
                    return true;
                else
                    return false;
        }
        return false;
    }

    void TestCardinalDirections(int x, int y)
    {
        int truthCounter = 0;

        if (TestDirection(x, y, 1))
        {
            SetVisited(x, y + 1);
            open.Add(gridArray[x, y + 1]);
            truthCounter++;
        }
        if (TestDirection(x, y, 2))
        {
            SetVisited(x, y - 1);
            open.Add(gridArray[x, y + 1]);
            truthCounter++;
        }
        if (TestDirection(x, y, 3))
        {
            SetVisited(x + 1, y);
            open.Add(gridArray[x, y + 1]);
            truthCounter++;
        }
        if (TestDirection(x, y, 4))
        {
            SetVisited(x - 1, y);
            open.Add(gridArray[x, y + 1]);
            truthCounter++;
        }

        if(truthCounter ==  0)
        {
            gridArray[x, y].GetComponent<TileInfo>().visited = 1;
            closed.Add(gridArray[x, y]);
            open.Remove(gridArray[x, y]);
        }
    }

    void SetVisited(int x, int y)
    {
        if(gridArray[x, y])
        {
            gridArray[x, y].GetComponent<TileInfo>().visited = 0;
        }
    }

    void PathFinding()
    {
        while(open.Count > 0 && !targetFound)
        {
            TestCardinalDirections(open[0].GetComponent<TileInfo>().x, open[0].GetComponent<TileInfo>().y);
            open[0].GetComponent<TileInfo>().visited = 1;
            closed.Add(open[0]);
            open.Remove(open[0]);
            foreach (GameObject obj in open)
            {
                
            }
        }
    }

    void SortList(List<GameObject> oc)
    {
        GameObject tmp;

        for (int i = 0; i < oc.Count; ++i)
        {
            for (int j = 1; j < oc.Count - 1; ++j)
            {
                if (oc[j].GetComponent<TileInfo>().costSoFar < oc[j - 1].GetComponent<TileInfo>().costSoFar)
                {
                    tmp = oc[j];
                    oc[j] = oc[j - 1];
                    oc[j - 1] = tmp;
                }
            }
        }
    }
}
