using System;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class Entity_Health : MonoBehaviour , IDamagable
{
    private Slider healthBar;
    private Entity_VFX entityVfx;
    private Entity entity;

    [SerializeField] protected float currentHp;
    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected bool isDead;


    [Header("¹¥»÷»÷ÍËÖµ")]
    [SerializeField] private float knockbackDuration = .2f;
    [SerializeField] private Vector2 knockbackPower = new Vector2(1.5f, 2.5f);
    [Header("ÖØ¹¥»÷»÷ÍËÖµ")]
    [Range(0f, 1f)]
    [SerializeField] private float heavyDamageThreshold = .3f;
    [SerializeField] private float heavyKnockbackDuration = .5f;
    [SerializeField] private Vector2 heavyKnockbackPower = new Vector2(7, 7);

    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();
        entityVfx = GetComponent<Entity_VFX>();
        healthBar = GetComponentInChildren<Slider>();

        currentHp = maxHp;
        UpdateHealthBar();
    }

    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if(isDead) return;

        float duration = CalculateDuration(damage);
        Vector2 knockback = CalculateKnockback(damage,damageDealer);

        entityVfx?.PlayOnDamageVfx();
        entity?.ReciveKnockback(knockback, duration);
        ReduceHp(damage);
    }

    protected void ReduceHp(float damage)
    {
        currentHp -= damage;
        UpdateHealthBar();

        if (currentHp < 0)
            Die();
    }

    private void Die()
    {
        isDead = true;
        entity?.EntityDeath();
    }

    private void UpdateHealthBar()
    {
        if (healthBar == null)
            return;

        healthBar.value = currentHp / maxHp;
    }
    

    private Vector2 CalculateKnockback(float damage, Transform damageDealer)
    {
        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;

        Vector2 knockback = IsHeavyDamage(damage) ? heavyKnockbackPower : knockbackPower;

        knockback.x *= direction;
        return knockback;
    }

    private float CalculateDuration(float damage) => IsHeavyDamage(damage) ? heavyKnockbackDuration : knockbackDuration;
    

    private bool IsHeavyDamage(float damage) => damage / maxHp > heavyDamageThreshold;
}
