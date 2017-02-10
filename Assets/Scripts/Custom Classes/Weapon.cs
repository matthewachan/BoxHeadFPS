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

    public void upgradeCapacity(int max_ammo) {
        m_MaxAmmo = max_ammo;
    }

    public void updateAmmo(int current_ammo) {
        m_CurrentAmmo = current_ammo;
    }

    public void updateFireRate(float fire_rate) {
        m_FireRate = fire_rate;
    }

    public void upgradeDamage(int attack_damage) {
        m_AttackDamage = attack_damage;
    }


    /* Accessors */
    public string getName() {
        return m_Name;
    }

    public bool isUnlocked() {
        return m_Unlocked;
    }

    public int getMaxAmmo() {
        return m_MaxAmmo;
    }

    public int getCurrentAmmo() {
        return m_CurrentAmmo;
    }

    public int getAttackDamage() {
        return m_AttackDamage;
    }

    public float getFireRate() {
        return m_FireRate;
    }

}
