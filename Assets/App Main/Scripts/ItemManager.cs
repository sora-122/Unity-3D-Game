// ItemManager.cs
// アイテム管理クラス
using UnityEngine;
using System.IO;

public class ItemManager : MonoBehaviour
{
    public ItemDataList itemDataList;

    private void Awake()
    {
        LoadItemData();
    }

    private void LoadItemData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "items.json");
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            itemDataList = JsonUtility.FromJson<ItemDataList>(dataAsJson);
            Debug.Log("Loaded " + itemDataList.items.Length + " items.");
        }
        else
        {
            Debug.LogError("Cannot load item data!");
        }
    }
}
