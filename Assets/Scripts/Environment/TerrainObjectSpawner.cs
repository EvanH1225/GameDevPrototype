using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainObjectSpawner : MonoBehaviour
{
    public Terrain terrain;
    public float heightOffset = 0.5f;

    public List<GameObject> objects;
    public int objectFrequency = 500;

    private Transform parentFolder;

    // Start is called before the first frame update
    void Start()
    {
        SpawnObjects();
    }

    // Update is called once per frame
    void SpawnObjects()
    {
        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainPos = terrain.GetPosition();

        GameObject existingFolder = GameObject.Find("SpawnedObjects");
        if (existingFolder == null)
        {
            existingFolder = new GameObject("SpawnedObjects");
        }
        parentFolder = existingFolder.transform;

        foreach(var obj in objects)
        {
            Transform objGroup = new GameObject(obj.name + "_Group").transform;
            objGroup.SetParent(parentFolder);

            for(int i = 0; i < objectFrequency; i++)
            {
                float randomX = Random.Range(0, terrainData.size.x);
                float randomZ = Random.Range(0, terrainData.size.z);
                float height = terrainData.GetInterpolatedHeight(randomX / terrainData.size.x, randomZ / terrainData.size.z);

                randomX += terrainPos.x;
                randomZ += terrainPos.z;
                height += terrainPos.y + heightOffset;

                Vector3 spawnPos = new Vector3(randomX, height, randomZ);
                Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);

                var instance = Instantiate(obj, spawnPos, rotation);
                instance.transform.SetParent(objGroup);
                instance.tag = "Pickup";
            }
        }
    }
}
