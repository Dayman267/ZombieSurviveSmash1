using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGenerator : MonoBehaviour
{
    public Grid grid;

    public Tilemap[] tilemapPrefabs;

    public Tilemap startingTilemap;
    private Tilemap[,] spawnedTilemaps;

    void Start()
    {
        spawnedTilemaps = new Tilemap[1000, 1000];
        spawnedTilemaps[500, 500] = startingTilemap;
        CheckAndSpawnTilemapsAround();
    }

    private void CheckAndSpawnTilemapsAround()
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
        for (int x = 0; x < spawnedTilemaps.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedTilemaps.GetLength(1); y++)
            {
                if (spawnedTilemaps[x, y] == null) continue;

                int maxX = spawnedTilemaps.GetLength(0) - 1;
                int maxY = spawnedTilemaps.GetLength(1) - 1;

                if (x > 0 && spawnedTilemaps[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                if (y > 0 && spawnedTilemaps[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                if (x > 0 && y > 0 && spawnedTilemaps[x - 1, y - 1] == null) vacantPlaces.Add(new Vector2Int(x - 1, y - 1));
                if (x < maxX && spawnedTilemaps[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                if (y < maxY && spawnedTilemaps[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));
                if (x < maxX && y < maxY && spawnedTilemaps[x + 1, y + 1] == null) vacantPlaces.Add(new Vector2Int(x + 1, y + 1));
                if (x > 0 && y < maxY && spawnedTilemaps[x - 1, y + 1] == null) vacantPlaces.Add(new Vector2Int(x - 1, y + 1));
                if (y > 0 && x < maxX && spawnedTilemaps[x + 1, y - 1] == null) vacantPlaces.Add(new Vector2Int(x + 1, y - 1));
            }
        }

        for (int i = vacantPlaces.Count; i > 0; i--)
        {
            Tilemap newTilemap = Instantiate(tilemapPrefabs[Random.Range(0, tilemapPrefabs.Length)]);
            newTilemap.transform.SetParent(grid.transform);
            Vector2Int position = vacantPlaces.ElementAt(i - 1);
            newTilemap.transform.position = new Vector3(position.x - 500, position.y - 500, 0) * 25;

            spawnedTilemaps[position.x, position.y] = newTilemap;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float positionX = (collision.gameObject.transform.position.x / 25) + 500;
        float positionY = (collision.gameObject.transform.position.y / 25) + 500;

        if (collision.gameObject.tag.Equals("Tilemap"))
        {
            HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();

            int maxX = spawnedTilemaps.GetLength(0) - 1;
            int maxY = spawnedTilemaps.GetLength(1) - 1;

            if (positionX > 0 && spawnedTilemaps[(int)positionX - 1, (int)positionY] == null) vacantPlaces.Add(new Vector2Int((int)positionX - 1, (int)positionY));
            if (positionY > 0 && spawnedTilemaps[(int)positionX, (int)positionY - 1] == null) vacantPlaces.Add(new Vector2Int((int)positionX, (int)positionY - 1));
            if (positionX > 0 && positionY > 0 && spawnedTilemaps[(int)positionX - 1, (int)positionY - 1] == null) vacantPlaces.Add(new Vector2Int((int)positionX - 1, (int)positionY - 1));
            if (positionX < maxX && spawnedTilemaps[(int)positionX + 1, (int)positionY] == null) vacantPlaces.Add(new Vector2Int((int)positionX + 1, (int)positionY));
            if (positionY < maxY && spawnedTilemaps[(int)positionX, (int)positionY + 1] == null) vacantPlaces.Add(new Vector2Int((int)positionX, (int)positionY + 1));
            if (positionX < maxX && positionY < maxY && spawnedTilemaps[(int)positionX + 1, (int)positionY + 1] == null) vacantPlaces.Add(new Vector2Int((int)positionX + 1, (int)positionY + 1));
            if (positionX > 0 && positionY < maxY && spawnedTilemaps[(int)positionX - 1, (int)positionY + 1] == null) vacantPlaces.Add(new Vector2Int((int)positionX - 1, (int)positionY + 1));
            if (positionY > 0 && positionX < maxX && spawnedTilemaps[(int)positionX + 1, (int)positionY - 1] == null) vacantPlaces.Add(new Vector2Int((int)positionX + 1, (int)positionY - 1));

            for (int i = vacantPlaces.Count; i > 0; i--)
            {
                Tilemap newTilemap = Instantiate(tilemapPrefabs[Random.Range(0, tilemapPrefabs.Length)]);
                newTilemap.transform.SetParent(grid.transform);
                Vector2Int position = vacantPlaces.ElementAt(i - 1);
                newTilemap.transform.position = new Vector3(position.x - 500, position.y - 500, 0) * 25;

                spawnedTilemaps[position.x, position.y] = newTilemap;
            }

            Destroy(collision.gameObject.GetComponent<BoxCollider2D>());
        }
    }
}
