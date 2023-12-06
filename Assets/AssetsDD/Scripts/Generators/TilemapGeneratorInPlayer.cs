using UnityEngine;
using Mirror;

public class TilemapGeneratorInPlayer : NetworkBehaviour
{
    private TilemapGeneratorInMapGenerator tilemapGeneratorInMapGenerator;

    public override void OnStartClient()
    {
        base.OnStartClient();
        
        tilemapGeneratorInMapGenerator = GameObject.
            FindWithTag("TileObjectsGenerator").
            GetComponent<TilemapGeneratorInMapGenerator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.localPosition == Vector3.zero || 
            collision.gameObject.transform.position == Vector3.zero) return;
        Vector2 position = new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y);
        string tag = collision.gameObject.tag;
        Component boxCollider2D = collision.gameObject.GetComponent<BoxCollider2D>();
        CmdSpawnGameObjectsOnTriggerEnter2D(position, tag, boxCollider2D);
    }

    [Command]
    void CmdSpawnGameObjectsOnTriggerEnter2D(Vector2 position, string tag, Component boxCollider2D)
    {
        tilemapGeneratorInMapGenerator.SpawnGameObjectsOnTriggerEnter2D(position, tag, boxCollider2D);
    }
}