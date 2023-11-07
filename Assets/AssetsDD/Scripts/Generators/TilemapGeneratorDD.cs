using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mirror;
using UnityEngine.Serialization;

public class TilemapGeneratorDD : NetworkBehaviour
{
    public GameObject map;
    
    public GameObject[] gameObjectPrefabs;
    
    public GameObject startingGameObject;
    private GameObject[,] spawnedGameObjects;

    private int objectSize = 5;
    
    void Start()
    {
        spawnedGameObjects = new GameObject[1000, 1000];
        spawnedGameObjects[500, 500] = startingGameObject;
        CheckAndSpawnTilemapsAround();
    }
    
    private void CheckAndSpawnTilemapsAround()
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
        for (int x = 0; x < spawnedGameObjects.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedGameObjects.GetLength(1); y++)
            {
                if (spawnedGameObjects[x, y] == null) continue;
    
                int maxX = spawnedGameObjects.GetLength(0) - 1;
                int maxY = spawnedGameObjects.GetLength(1) - 1;
    
                if (x > 0 && spawnedGameObjects[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                if (y > 0 && spawnedGameObjects[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                if (x > 0 && y > 0 && spawnedGameObjects[x - 1, y - 1] == null) vacantPlaces.Add(new Vector2Int(x - 1, y - 1));
                if (x < maxX && spawnedGameObjects[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                if (y < maxY && spawnedGameObjects[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));
                if (x < maxX && y < maxY && spawnedGameObjects[x + 1, y + 1] == null) vacantPlaces.Add(new Vector2Int(x + 1, y + 1));
                if (x > 0 && y < maxY && spawnedGameObjects[x - 1, y + 1] == null) vacantPlaces.Add(new Vector2Int(x - 1, y + 1));
                if (y > 0 && x < maxX && spawnedGameObjects[x + 1, y - 1] == null) vacantPlaces.Add(new Vector2Int(x + 1, y - 1));
            }
        }
    
        for (int i = vacantPlaces.Count; i > 0; i--)
        {
            GameObject newGameObject = Instantiate(gameObjectPrefabs[Random.Range(0, gameObjectPrefabs.Length)]);
            newGameObject.transform.SetParent(map.transform);
            Vector2Int position = vacantPlaces.ElementAt(i - 1);
            newGameObject.transform.position = new Vector3(position.x - 500, position.y - 500, 0) * objectSize;
    
            spawnedGameObjects[position.x, position.y] = newGameObject;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        float positionX = (collision.gameObject.transform.position.x / objectSize) + 500;
        float positionY = (collision.gameObject.transform.position.y / objectSize) + 500;
    
        if (collision.gameObject.tag.Equals("Tilemap"))
        {
            HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
    
            int maxX = spawnedGameObjects.GetLength(0) - 1;
            int maxY = spawnedGameObjects.GetLength(1) - 1;
    
            if (positionX > 0 && spawnedGameObjects[(int)positionX - 1, (int)positionY] == null) vacantPlaces.Add(new Vector2Int((int)positionX - 1, (int)positionY));
            if (positionY > 0 && spawnedGameObjects[(int)positionX, (int)positionY - 1] == null) vacantPlaces.Add(new Vector2Int((int)positionX, (int)positionY - 1));
            if (positionX > 0 && positionY > 0 && spawnedGameObjects[(int)positionX - 1, (int)positionY - 1] == null) vacantPlaces.Add(new Vector2Int((int)positionX - 1, (int)positionY - 1));
            if (positionX < maxX && spawnedGameObjects[(int)positionX + 1, (int)positionY] == null) vacantPlaces.Add(new Vector2Int((int)positionX + 1, (int)positionY));
            if (positionY < maxY && spawnedGameObjects[(int)positionX, (int)positionY + 1] == null) vacantPlaces.Add(new Vector2Int((int)positionX, (int)positionY + 1));
            if (positionX < maxX && positionY < maxY && spawnedGameObjects[(int)positionX + 1, (int)positionY + 1] == null) vacantPlaces.Add(new Vector2Int((int)positionX + 1, (int)positionY + 1));
            if (positionX > 0 && positionY < maxY && spawnedGameObjects[(int)positionX - 1, (int)positionY + 1] == null) vacantPlaces.Add(new Vector2Int((int)positionX - 1, (int)positionY + 1));
            if (positionY > 0 && positionX < maxX && spawnedGameObjects[(int)positionX + 1, (int)positionY - 1] == null) vacantPlaces.Add(new Vector2Int((int)positionX + 1, (int)positionY - 1));
    
            for (int i = vacantPlaces.Count; i > 0; i--)
            {
                GameObject newGameObject = Instantiate(gameObjectPrefabs[Random.Range(0, gameObjectPrefabs.Length)]);
                newGameObject.transform.SetParent(map.transform);
                Vector2Int position = vacantPlaces.ElementAt(i - 1);
                newGameObject.transform.position = new Vector3(position.x - 500, position.y - 500, 0) * objectSize;
    
                spawnedGameObjects[position.x, position.y] = newGameObject;
            }
    
            Destroy(collision.gameObject.GetComponent<BoxCollider2D>());
        }
    }
}
