// ItemData.cs
// アイテムデータのクラス
using System;

[Serializable]
public class ItemData
{
    public string id;
    public string name;
    public string type; // "weapon", "armor"
    public int grade; // 1, 2, 3
    public float damage;
    public float range;
    // ... 他のステータス
    public float defense;
}

[Serializable]
public class ItemDataList
{
    public ItemData[] items;
}
