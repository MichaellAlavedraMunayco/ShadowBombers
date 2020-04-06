using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevel : MonoBehaviour
{
    public Vector3 start;
    public Vector3 offset;
    [Header("Prefabs")]
    public GameObject metal;
    public GameObject brick;
    public GameObject heart;
    [Header("Placeholder")]
    public Transform metalContainer;
    public Transform bricksContainer;
    public Transform heartsContainer;
    [Header("Dimensions")]
    public int width = 17;
    public int height = 11;

    int dist = 2;
    int initialSpace = 3;

    void Awake()
    {
        BuildMetal();
        BuildBricks();
    }

    void BuildMetal()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (i == 0 || i == width - 1)
                {
                    Instantiate(metal, new Vector3(start.x + i + offset.x, start.y + offset.y, start.z + j + offset.z), metal.transform.rotation, metalContainer);
                }
                if (j == 0 || j == height - 1)
                {
                    Instantiate(metal, new Vector3(start.x + i + offset.x, start.y + offset.y, start.z + j + offset.z), metal.transform.rotation, metalContainer);
                }
            }
        }

        for (int i = dist; i < width - dist; i++)
        {
            for (int j = dist; j < height - dist; j++)
            {
                if ((i % dist) == 0 && (j % dist) == 0)
                {
                    Instantiate(metal, new Vector3(start.x + i + offset.x, start.y + offset.y, start.z + j + offset.z), metal.transform.rotation, metalContainer);
                }
            }
        }
    }

    void BuildBricks()
    {

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (
                    (((i % dist) == 0 && (j % dist) == 0)) ||
                    (i == 0 || i == width - 1) ||
                    (j == 0 || j == height - 1) ||
                    (i < initialSpace && j < initialSpace) ||
                    (i < initialSpace && j > height - 1 - initialSpace) ||
                    (i > width - 1 - initialSpace && j < initialSpace) ||
                    (i > width - 1 - initialSpace && j > height - 1 - initialSpace))
                {
                    continue;
                }
                if (Random.Range(0f, 1f) <= 0.75f)
                {
                    Instantiate(brick, new Vector3(start.x + i + offset.x, start.y + offset.y, start.z + j + offset.z), brick.transform.rotation, bricksContainer);
                }
                else
                {
                    if (Random.Range(0f, 1f) >= 0.75f)
                    {
                        Instantiate(heart, new Vector3(start.x + i + offset.x, start.y + offset.y, start.z + j + offset.z), heart.transform.rotation, heartsContainer);
                    }
                }
            }
        }
    }
}
