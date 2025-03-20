// Sword.cs
// 剣の実装
using UnityEngine;

[CreateAssetMenu(fileName = "New Sword", menuName = "Items/Weapon/Sword")]
public class Sword : WeaponBase
{
    public float damage = 10f;
    public float range = 1f;

    public override void Use()
    {
        Debug.Log("Swing Sword! Damage: " + damage + ", Range: " + range);
        // 剣を使った攻撃処理
    }
}
