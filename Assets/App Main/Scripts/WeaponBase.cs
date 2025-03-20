// WeaponBase.cs
// 武器の抽象クラス
using UnityEngine;
public abstract class WeaponBase : ScriptableObject, IWeapon
{
    public string WeaponName { get; protected set; }
    public abstract void Use();
}