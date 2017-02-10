using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Component {
    private string m_Name;
    private bool m_Unlocked;
    private int m_MaxAmmo;
    private int m_CurrentAmmo;
    private float m_FireRate;
    private int m_AttackDamage;



    public Weapon(string name, bool unlocked, int current_ammo, int max_ammo, int attack_damage, float fire_rate) {
        m_Name = name;
        m_Unlocked = unlocked;
        m_CurrentAmmo = current_ammo;
        m_MaxAmmo = max_ammo;
        m_FireRate = fire_rate;
        m_AttackDamage = attack_damage;
    }

    public void UpgradeCapacity(int max_ammo) {
        m_MaxAmmo = max_ammo;
    }

    public void UpdateAmmo(int current_ammo) {
        m_CurrentAmmo = current_ammo;
    }

    public void UpdateFireRate(float fire_rate) {
        m_FireRate = fire_rate;
    }

    public void UpgradeDamage(int attack_damage) {
        m_AttackDamage = attack_damage;
    }


    /* Accessors */
    public string GetName() {
        return m_Name;
    }

    public bool IsUnlocked() {
        return m_Unlocked;
    }

    public int GetMaxAmmo() {
        return m_MaxAmmo;
    }

    public int GetCurrentAmmo() {
        return m_CurrentAmmo;
    }

    public int GetAttackDamage() {
        return m_AttackDamage;
    }

    public float GetFireRate() {
        return m_FireRate;
    }

}
