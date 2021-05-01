using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public event System.Action OnDamage;
    public event System.Action OnHeal;
    public event System.Action OnStomp;

    [SerializeField]
    private float mHealAmount;

    [SerializeField]
    private float mHealth = 100;
    private float mMaxHealth;

    public float Health { get { return mHealth; } set { mHealth = value; } }
    public float MaxHealth { get { return mMaxHealth; } }

    private void Start()
    {
        mMaxHealth = mHealth;
    }

    public void playerHeal()
    {
        OnHeal?.Invoke();
        if(Health < MaxHealth)
        {
            Health += mHealAmount;
            if (Health > MaxHealth)
                Health = MaxHealth;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        DeathZone deathZone = collision.collider.GetComponent<DeathZone>();
        if (enemy != null)
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                //Debug.DrawLine(point.point, point.point + point.normal, Color.red, 10);
                if (point.normal.y >= 0.9f)
                {
                    OnStomp?.Invoke();
                    enemy.Hurt();
                }
                else
                    takeDamage(enemy.Damage);
            }
        }
        if(deathZone!= null)
        {
            Respawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Goal goal = collision.GetComponent<Goal>();
        if (goal != null)
        {
            goal.Activate();
            Debug.Log("Reached goal!");
        }
    }

    private void takeDamage(float damage)
    {
        OnDamage?.Invoke();
        Health -= damage;
        if (Health <= 0)
            Respawn();
    }

    private void Respawn()
    {
        SpawnPoint spawn = FindObjectOfType<SpawnPoint>();
        this.transform.position =
            new Vector3(spawn.transform.position.x, spawn.transform.position.y);
        Health = MaxHealth;
    }

    public float calculateHealth()
    {
        return Health / MaxHealth;
    }

    
}
