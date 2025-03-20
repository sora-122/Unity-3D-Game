// IWeapon.cs
// 武器が実装するインターフェース
public interface IWeapon
{
    string WeaponName { get; }
    void Use();
}