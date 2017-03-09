using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Component {
    private string m_Name;
    private Rigidbody m_BulletPrefab;
    private bool m_Unlocked;
    private int m_MaxAmmo;
    private int m_CurrentAmmo;
    private int m_AttackDamage;
    private float m_FireRate;
    private float m_BulletSpeed;
    private bool m_IsProjectile;



    public Weapon(string name, Rigidbody bulletPrefab, bool unlocked, int currentAmmo, int maxAmmo, int attackDamage, float fireRate, float bulletSpeed, bool isProjectile) {
        m_Name = name;
        m_BulletPrefab = bulletPrefab;
        m_Unlocked = unlocked;
        m_CurrentAmmo = currentAmmo;
        m_MaxAmmo = maxAmmo;
        m_FireRate = fireRate;
        m_AttackDamage = attackDamage;
        m_BulletSpeed = bulletSpeed;
        m_IsProjectile = isProjectile;
    }

    public void UpgradeCapacity(int maxAmmo) {
        m_MaxAmmo = maxAmmo;
    }

    public void UpdateAmmo(int currentAmmo) {
        m_CurrentAmmo = currentAmmo;
    }

    public void UpdateFireRate(float fireRate) {
        m_FireRate = fireRate;
    }

    public void UpdateBulletSpeed(float bulletSpeed) {
        m_BulletSpeed = bulletSpeed;
    }

    public void UpgradeDamage(int attackDamage) {
        m_AttackDamage = attackDamage;
    }

    public void UnlockWeapon() {
        m_Unlocked = true;
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

    public float GetBulletSpeed() {
        return m_BulletSpeed;
    }

    public bool IsProjectile() {
        return m_IsProjectile;
    }

    public Rigidbody GetPrefab() {
        return m_BulletPrefab;
    }

}
