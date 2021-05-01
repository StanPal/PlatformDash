using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Slider healthBar;
    private PlayerStats mPlayerStats;

    private void Awake()
    {
        mPlayerStats = FindObjectOfType<PlayerStats>();
        mPlayerStats.OnDamage += UpdateHealthBar;
        mPlayerStats.OnHeal += UpdateHealthBar;
    }

    private void UpdateHealthBar()
    {
        healthBar.value = mPlayerStats.calculateHealth();
    }

    private void Update()
    {
        healthBar.value = mPlayerStats.calculateHealth();
    }

}
