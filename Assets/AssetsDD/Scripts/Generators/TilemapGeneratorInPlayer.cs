using System;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class TilemapGeneratorInPlayer : NetworkBehaviour
{
    private TilemapGeneratorInMapGenerator tilemapGeneratorInMapGenerator;
    private PointsManager manager;
    
    [SerializeField] private string sceneName = "DimaSceneDD";

    public override void OnStartClient()
    {
        base.OnStartClient();
        
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != sceneName)
        {
            Destroy(this);
            return;
        }
        
        tilemapGeneratorInMapGenerator = GameObject.
            FindWithTag("TileObjectsGenerator").
            GetComponent<TilemapGeneratorInMapGenerator>();
        manager = GameObject.FindWithTag("PointsManager").GetComponent<PointsManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.tag.Equals("TileObject") ||
            manager.counterICanEnter <= 0) return;
        if (collision.gameObject.transform.localPosition == Vector3.zero ||
            collision.gameObject.transform.position == Vector3.zero)
        {
            Destroy(collision.gameObject.GetComponent<EdgeCollider2D>());
            return;
        }
        CmdDestroyEdgeCollider(collision.gameObject);
        Vector2 position = new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y);
        CmdSpawnGameObjectsOnTriggerEnter2D(position);
        CmdSubtractOneCounterICanEnter();
    }

    [Command]
    void CmdDestroyEdgeCollider(GameObject obj)
    {
        RpcDestroyEdgeCollider(obj);
    }

    [ClientRpc]
    void RpcDestroyEdgeCollider(GameObject obj)
    {
        Destroy(obj.GetComponent<EdgeCollider2D>());
    }
    

    [Command]
    void CmdSpawnGameObjectsOnTriggerEnter2D(Vector2 position)
    {
        tilemapGeneratorInMapGenerator.SpawnGameObjectsOnTriggerEnter2D(position);
    }
    
    [Command]
    void CmdSubtractOneCounterICanEnter()
    {
        manager.RpcSubtractOneCounterICanEnter();
    }
}