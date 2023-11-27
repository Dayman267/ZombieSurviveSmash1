using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mirror;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using System.IO;

public class TilemapGeneratorDD : NetworkBehaviour
{
    public GameObject parentToSet;
    
    public GameObject[] gameObjectPrefabs;
    
    SyncList<int> syncGameObjectsIds = new SyncList<int>();
    [FormerlySerializedAs("gameObjectIds")] public List<int> gameObjectsIds;
    public int[,] gameObjectsToSpawn;

    [SyncVar(hook = nameof(SyncSeed))]
    int syncSeed;
    public int seed;
    private System.Random random;
    public Item item;
    
    private int width = 10;         // must be even
    private int height = 10;        // must be even

    private int objectSize = 5;

    public override void OnStartServer()
    {
        base.OnStartServer();
        item = JsonUtility.FromJson<Item>(File.ReadAllText(Application.streamingAssetsPath + "/config.json"));
        syncSeed = item.Seed; 
        Debug.Log(seed);
        random = new System.Random(seed);
        parentToSet = GameObject.FindWithTag("TileObjects");
        gameObjectsToSpawn = new int[width, height];
        CheckAndSpawnGameObjectsAround();
    }

    public void OnDisable()
    {
        if (isServer && isLocalPlayer)
        {
            item.Seed += 1;
            File.WriteAllText(Application.streamingAssetsPath + "/config.json", JsonUtility.ToJson(item));
        }
    }

    void SyncSeed(int oldValue, int newValue) //обязательно делаем два значения - старое и новое. 
    {
        seed = newValue;
    }
    
    [Server]
    private void CheckAndSpawnGameObjectsAround()
    {
        int index = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == width / 2 && y == height / 2) syncGameObjectsIds.Add(0);
                else syncGameObjectsIds.Add(-1);
                gameObjectsToSpawn[x, y] = syncGameObjectsIds[index];
                index++;
            }
        }

        index = 0;
        bool isFinished = false;
        for (int x = 1; x < width-1 && !isFinished; x++)
        {
            for (int y = 1; y < height-1 && !isFinished; y++)
            {
                if (gameObjectsToSpawn[x, y] == -1)
                {
                    index++;
                    continue;
                }
    
                int maxX = width - 1;
                int maxY = height - 1;
                
                if (x > 0 && gameObjectsToSpawn[x - 1,y] == -1)
                {
                    gameObjectsToSpawn[x - 1,y] = random.Next(0, 3);
                }
                if (y > 0 && gameObjectsToSpawn[x,y - 1] == -1)
                {
                    gameObjectsToSpawn[x,y - 1] = random.Next(0, 3);
                }
                if (x > 0 && y > 0 && gameObjectsToSpawn[x - 1,y - 1] == -1) 
                {
                    gameObjectsToSpawn[x - 1,y - 1] = random.Next(0, 3);       
                }
                if (x < maxX && gameObjectsToSpawn[x + 1,y] == -1) 
                {
                    gameObjectsToSpawn[x + 1,y] = random.Next(0, 3);       
                }
                if (y < maxY && gameObjectsToSpawn[x,y + 1] == -1) 
                {
                    gameObjectsToSpawn[x,y + 1] = random.Next(0, 3);       
                }
                if (x < maxX && y < maxY && gameObjectsToSpawn[x + 1,y + 1] == -1) 
                {
                    gameObjectsToSpawn[x + 1,y + 1] = random.Next(0, 3);       
                }
                if (x > 0 && y < maxY && gameObjectsToSpawn[x - 1,y + 1] == -1) 
                {
                    gameObjectsToSpawn[x - 1,y + 1] = random.Next(0, 3);       
                }
                if (y > 0 && x < maxX && gameObjectsToSpawn[x + 1,y - 1] == -1)
                {
                    gameObjectsToSpawn[x + 1,y - 1] = random.Next(0, 3);
                }
                isFinished = true;
            }
        }

        index = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                syncGameObjectsIds[index] = gameObjectsToSpawn[x, y];
                index++;
            }
        }
    }

    [Command]
    void CmdCheckAndSpawnGameObjectsAround()
    {
        CheckAndSpawnGameObjectsAround();
    }

    [ClientRpc]
    void SpawnObjectsAround()
    {
        if (isOwned)
        {
            for (int x = 0, index = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    gameObjectsToSpawn[x, y] = gameObjectsIds[index];
                    if (gameObjectsToSpawn[x, y] == -1)
                    {
                        index++;
                        continue;
                    }
                    Instantiate(gameObjectPrefabs[gameObjectsToSpawn[x, y]],
                        new Vector3(x-width/2, y-height/2, 0) * objectSize, Quaternion.identity, parentToSet.transform);
                    index++;
                }
            }
        }
    }

    [Command]
    void CmdSpawnObjectsAround()
    {
        SpawnObjectsAround();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.localPosition == Vector3.zero || 
            collision.gameObject.transform.position == Vector3.zero) return;
        Vector2 position = new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y);
        string tag = collision.gameObject.tag;
        Component boxCollider2D = collision.gameObject.GetComponent<BoxCollider2D>();
        if (isServer) SpawnGameObjectsOnTriggerEnter2D(position, tag, boxCollider2D);
        else CmdSpawnGameObjectsOnTriggerEnter2D(position, tag, boxCollider2D);
    }
    
    [ClientRpc]
    private void SpawnGameObjectsOnTriggerEnter2D(Vector2 position, string tag, Component boxCollider2D)
    {
        Destroy(boxCollider2D);
        
        float positionX = (position.x / objectSize) + width/2;
        float positionY = (position.y / objectSize) + height/2;
    
        if (tag.Equals("TileObject"))
        {
    
            int maxX = gameObjectsToSpawn.GetLength(0) - 1;
            int maxY = gameObjectsToSpawn.GetLength(1) - 1;

            if (positionX > 0 && gameObjectsToSpawn[(int)positionX - 1, (int)positionY] == -1)
            {
                gameObjectsToSpawn[(int)positionX - 1,(int)positionY] = random.Next(0, 3);
                Instantiate(gameObjectPrefabs[gameObjectsToSpawn[(int)positionX - 1,(int)positionY]],
                    new Vector3((int)positionX - 1-width/2, (int)positionY-height/2, 0) * objectSize,
                    Quaternion.identity, parentToSet.transform);
            }
            if (positionY > 0 && gameObjectsToSpawn[(int)positionX, (int)positionY - 1] == -1)
            {
                gameObjectsToSpawn[(int)positionX, (int)positionY - 1] = random.Next(0, 3);
                Instantiate(gameObjectPrefabs[gameObjectsToSpawn[(int)positionX,(int)positionY - 1]],
                    new Vector3((int)positionX -width/2, (int)positionY - 1-height/2, 0) * objectSize,
                    Quaternion.identity, parentToSet.transform);
            }
            if (positionX > 0 && positionY > 0 && gameObjectsToSpawn[(int)positionX - 1, (int)positionY - 1] == -1) 
            {
                gameObjectsToSpawn[(int)positionX - 1, (int)positionY - 1] = random.Next(0, 3);
                Instantiate(gameObjectPrefabs[gameObjectsToSpawn[(int)positionX - 1,(int)positionY -1]],
                    new Vector3((int)positionX - 1-width/2, (int)positionY-1-height/2, 0) * objectSize,
                    Quaternion.identity, parentToSet.transform);
            }
            if (positionX < maxX && gameObjectsToSpawn[(int)positionX + 1, (int)positionY] == -1) 
            {
                gameObjectsToSpawn[(int)positionX + 1, (int)positionY -1] = random.Next(0, 3);
                Instantiate(gameObjectPrefabs[gameObjectsToSpawn[(int)positionX + 1,(int)positionY-1]],
                    new Vector3((int)positionX + 1-width/2, (int)positionY-1-height/2, 0) * objectSize,
                    Quaternion.identity, parentToSet.transform);
            }
            if (positionY < maxY && gameObjectsToSpawn[(int)positionX, (int)positionY + 1] == -1) 
            {
                gameObjectsToSpawn[(int)positionX, (int)positionY + 1] = random.Next(0, 3);
                Instantiate(gameObjectPrefabs[gameObjectsToSpawn[(int)positionX,(int)positionY+1]],
                    new Vector3((int)positionX-width/2, (int)positionY+1-height/2, 0) * objectSize,
                    Quaternion.identity, parentToSet.transform);
            }
            if (positionX < maxX && positionY < maxY && gameObjectsToSpawn[(int)positionX + 1, (int)positionY + 1] == -1) 
            {
                gameObjectsToSpawn[(int)positionX + 1, (int)positionY + 1] = random.Next(0, 3);
                Instantiate(gameObjectPrefabs[gameObjectsToSpawn[(int)positionX + 1,(int)positionY+1]],
                    new Vector3((int)positionX + 1-width/2, (int)positionY+1-height/2, 0) * objectSize,
                    Quaternion.identity, parentToSet.transform);
            }
            if (positionX > 0 && positionY < maxY && gameObjectsToSpawn[(int)positionX - 1, (int)positionY + 1] == -1) 
            {
                gameObjectsToSpawn[(int)positionX - 1, (int)positionY + 1] = random.Next(0, 3);
                Instantiate(gameObjectPrefabs[gameObjectsToSpawn[(int)positionX - 1,(int)positionY+1]],
                    new Vector3((int)positionX - 1-width/2, (int)positionY+1-height/2, 0) * objectSize,
                    Quaternion.identity, parentToSet.transform);
            }
            if (positionY > 0 && positionX < maxX && gameObjectsToSpawn[(int)positionX + 1, (int)positionY - 1] == -1) 
            {
                gameObjectsToSpawn[(int)positionX + 1, (int)positionY - 1] = random.Next(0, 3);
                Instantiate(gameObjectPrefabs[gameObjectsToSpawn[(int)positionX + 1,(int)positionY-1]],
                    new Vector3((int)positionX + 1-width/2, (int)positionY-1-height/2, 0) * objectSize,
                    Quaternion.identity, parentToSet.transform);
            }
        }
    }
    
    [Command]
    private void CmdSpawnGameObjectsOnTriggerEnter2D(Vector2 position, string tag, Component boxCollider2D)
    {
        SpawnGameObjectsOnTriggerEnter2D(position, tag, boxCollider2D);
    }
    
    void SyncListOfSpawnedGameObjects(SyncList<int>.Operation op, int index, int oldItem, int newItem)
    {
        switch (op)
        {
            case SyncList<int>.Operation.OP_ADD:
            {
                gameObjectsIds.Add(newItem);
                break;
            }
            case SyncList<int>.Operation.OP_CLEAR:
            {
                break;
            }
            case SyncList<int>.Operation.OP_INSERT:
            {
                break;
            }
            case SyncList<int>.Operation.OP_REMOVEAT:
            {
                break;
            }
            case SyncList<int>.Operation.OP_SET:
            {
                gameObjectsIds[index] = syncGameObjectsIds[index];
                break;
            }
        }
    }
    
    public override void OnStartClient()
    {
        base.OnStartClient();

        syncGameObjectsIds.Callback += SyncListOfSpawnedGameObjects;

        gameObjectsIds = new List<int>(syncGameObjectsIds.Count);
        gameObjectsToSpawn = new int[width, height];
        parentToSet = GameObject.FindWithTag("TileObjects");
        for (int i = 0; i < syncGameObjectsIds.Count; i++)
        {
            gameObjectsIds.Add(syncGameObjectsIds[i]);
        }
        random = new System.Random(seed);
        CmdSpawnObjectsAround();
    }
    
    [System.Serializable]
    public class Item
    {
        public int Seed;
    }
}