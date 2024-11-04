using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerAlt : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject asteroidParent;
    public float spawnWait;
    public int[,] mGrid;
    public GameObject gameArea;
    private float maxSize;
    private GameObject lastPlaced;
    private int saveX = -1;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 sz = gameArea.GetComponent<Renderer>().bounds.size;
        Vector3 asteroidSize = asteroid.GetComponent<Renderer>().bounds.size;
        float width = sz.x;
        float height = sz.z;
        float asteroidWidth = asteroidSize.x;
        float asteroidHeight = asteroidSize.z;
        if (asteroidWidth > asteroidHeight) maxSize = asteroidWidth; else maxSize = asteroidHeight;
        int gridWidth = (int)(width / maxSize);
        int gridHeight = (int)(height / maxSize);
        StartCoroutine(InitializeGrid(gridWidth, gridHeight));
    }

    IEnumerator InitializeGrid(int width, int height)
    {
        mGrid = new int[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                mGrid[i, j] = Random.Range(0, 2);
            }
        }

        Vector3 gameAreaPos = gameArea.transform.position;
        float topCornerZ = gameAreaPos.z + height / 2;

        CellularAutomata();
        for (int k = 0; k < 5000; k++)
        {
            yield return new WaitForSeconds(0.5f);
            if (lastPlaced.transform.position.z <= topCornerZ+2)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        mGrid[i, j] = Random.Range(0, 2);
                    }
                }
                CellularAutomata();
            }
        }
    }

    void CellularAutomata()
    {
        RandomWalk();
        Vector3 sz = gameArea.GetComponent<Renderer>().bounds.size;
        Vector3 asteroidSize = asteroid.GetComponent<Renderer>().bounds.size;
        Vector3 gameAreaPos = gameArea.transform.position;
        float topCornerX = gameAreaPos.x - sz.x / 2;
        float topCornerZ = gameAreaPos.z - sz.z / 2;
        for (int i = 0; i < mGrid.GetLength(0); i ++)
        {
            for(int j = 0; j < mGrid.GetLength(1); j++)
            {
                if (mGrid[i,j] == 1)
                {
                    // initialize asteroid here
                    float x = topCornerX + (float)i* sz.x/ (float)mGrid.GetLength(0)+maxSize/2;
                    float z = topCornerZ + (float)j* sz.z/ (float)mGrid.GetLength(1)+sz.z;
                    Vector3 spawnPosition = new Vector3(x, 0, z);
                    Transform asteroidTransform = asteroid.transform;
                    asteroidTransform.position = spawnPosition;
                    GameObject spawned = Instantiate(asteroid, asteroidTransform);
                    spawned.transform.parent = asteroidParent.transform;
                    lastPlaced = spawned;
                }
            }
        }
    }

    void RandomWalk()
    {
        int startX = 0;
        if (saveX == -1)
        {
            startX = Random.Range(0, mGrid.GetLength(0));
        }
        else
        {
            startX = saveX;
        }
        int startY = mGrid.GetLength(1)-1;
        mGrid[startX, startY] = 0;
        int moveChoice = 0;
        while(startY > 0)
        {
            Debug.Log(startX + " " + startY);
            moveChoice = Random.Range(0, 3);
            switch (moveChoice)
            {
                case 0:
                    startY -= 1;
                    mGrid[startX, startY] = 0;
                    break;
                case 1:
                    // left
                    if(startX > 1)
                    {
                        startX -= 1;
                        mGrid[startX, startY] = 0;
                        mGrid[startX - 1, startY] = 0;
                        mGrid[startX + 1, startY] = 0;
                    }
                    break;
                case 2:
                    // Right
                    if (startX < mGrid.GetLength(0)-2)
                    {
                        startX += 1;
                        mGrid[startX, startY] = 0;
                        mGrid[startX - 1, startY] = 0;
                        mGrid[startX + 1, startY] = 0;
                    }
                    break;
                default:
                    Debug.Log("Something went wrong");
                    break;
            }
        }
        saveX = startX;
    }
}
