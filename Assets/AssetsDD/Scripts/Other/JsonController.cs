using UnityEngine;
using System.IO;

public class JsonController : MonoBehaviour
{
    public Item item;

    [ContextMenu("Load")]
    public void LoadField()
    {
        item = JsonUtility.FromJson<Item>(File.ReadAllText(Application.streamingAssetsPath + "/config.json"));
    }

    [ContextMenu("Save")]
    public void SaveField()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/config.json", JsonUtility.ToJson(item));
    }
    
    [System.Serializable]
    public class Item
    {
        public int Seed;
    }
}
