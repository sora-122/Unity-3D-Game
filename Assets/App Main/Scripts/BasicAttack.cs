// BasicAttack.cs
// 基本攻撃の実装
using UnityEngine;

public class BasicAttack : MonoBehaviour, IAttackable
{
    public float attackRange = 1f;
    public float attackDamage = 10f;
    public LayerMask enemyLayer;

    public void Attack()
    {
        Debug.Log("Basic Attack!");
        // 攻撃処理（例：前方への攻撃判定）
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);
        foreach (Collider enemy in hitEnemies)
        {
            // 敵にダメージを与える処理（仮）
            Debug.Log("Hit Enemy: " + enemy.name);
            // ここに敵のダメージ処理を記述
        }
    }
}
