﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsControl : MonoBehaviour {

    [SerializeField]
    private Rigidbody m_BulletPrefab;
    [SerializeField]
    private Rigidbody m_ShotgunBulletPrefab;
    [SerializeField]
    private Rigidbody m_BarrelPrefab;
    [SerializeField]
    private Rigidbody m_GrenadePrefab;
    [SerializeField]
    private Rigidbody m_FakeWallsPrefab;
    [SerializeField]
    private Rigidbody m_ClaymorePrefab;
    [SerializeField]
    private Rigidbody m_RocketPrefab;
    [SerializeField]
    private Rigidbody m_ChargepackPrefab;
    [SerializeField]
    private Rigidbody m_RailgunPrefab;


    private NotificationControl m_Notification;
    private Weapon[] m_Weapons;
    private Weapon m_CurrentWeapon;
    private GameObject m_BulletSpawn;
    private GameObject m_NonProjectileSpawn;
    private UnityEngine.UI.Text m_WeaponText;
    private float m_CooldownTimestamp;


    // Use this for initialization
    void Start () {
        
        // Create list of weapons
        m_Weapons = new Weapon[10];
        m_Weapons[0] = new global::Weapon("Pistol", m_BulletPrefab, true, 50, 50, 10, .2f, 50, true);
        m_Weapons[1] = new global::Weapon("UZI", m_BulletPrefab, true, 100, 100, 10, .1f, 50, true);
        m_Weapons[2] = new global::Weapon("Shotgun", m_ShotgunBulletPrefab, true, 100, 100, 10, .1f, 50, true);
        m_Weapons[3] = new global::Weapon("Barrel", m_BarrelPrefab, true, 100, 100, 10, 2, 0, false);
        m_Weapons[4] = new global::Weapon("Grenade", m_GrenadePrefab, true, 100, 100, 10, 1f, 10, true);
        m_Weapons[5] = new global::Weapon("Fake Walls", m_FakeWallsPrefab, true, 100, 100, 2, 2, 0, false);
        m_Weapons[6] = new global::Weapon("Claymore", m_ClaymorePrefab, true, 100, 100, 10, 1, 0, false);
        m_Weapons[7] = new global::Weapon("Rocket", m_RocketPrefab, true, 100, 100, 10, .5f, 30, true);
        m_Weapons[8] = new global::Weapon("Chargepack", m_ChargepackPrefab, true, 100, 100, 10, 1, 0, false);
        m_Weapons[9] = new global::Weapon("Railgun", m_RailgunPrefab, true, 100, 100, 10, .1f, 50, true);


        // Setup HUD
        m_CurrentWeapon = m_Weapons[0];
        m_CooldownTimestamp = Time.time + m_CurrentWeapon.GetFireRate();
        m_WeaponText = GameObject.Find("Weapon").GetComponent<UnityEngine.UI.Text>();
        m_WeaponText.text = m_CurrentWeapon.GetName() + ": " + m_CurrentWeapon.GetCurrentAmmo();
        m_Notification = GameObject.Find("Notification").GetComponent<NotificationControl>();
        

        m_BulletSpawn = GameObject.Find("BulletSpawn");
        m_NonProjectileSpawn = GameObject.Find("Non-Projectile Spawn");
    }

    public void UpdateInventory(string name) {
        for ( int i = 0; i < m_Weapons.Length; ++i) {
            if (m_Weapons[i].GetName() == name)
                m_Weapons[i].UpdateAmmo(m_Weapons[i].GetMaxAmmo());
        }
    }
    
    public void UnlockWeapon(string weaponName, int inventoryKey) {
        
        for (int i = 0; i < m_Weapons.Length; ++i) {
            
            if (m_Weapons[i].GetName() == weaponName) {
                m_Weapons[i].UnlockWeapon();
                UnlockNotification(m_Weapons[i].GetName(), inventoryKey);
                
            }
        }
    }

    public string KeyToName(int key) {
        return m_Weapons[key].GetName();
    }



    public List<string> GetUnlockedWeapons() {
        List<string> unlockedWeapons = new List<string>();
        for (int i = 0; i < m_Weapons.Length; ++i) {
            if (m_Weapons[i].IsUnlocked())
                unlockedWeapons.Add(m_Weapons[i].GetName());
        }
        return unlockedWeapons;
    }


    public void UnlockNotification(string weapon, int key) {
        m_Notification.ChangeTextColor(Color.green);
        m_Notification.PostNotification("New Weapon: " + weapon + " (Key " + key + ")");
    }

    public Weapon GetCurrentWeapon() {
        return m_CurrentWeapon;
    }

    private void FixedUpdate() {
        if (Input.GetButton("Fire1") && GetCurrentWeapon().GetName() == "Chargepack" && GameObject.Find("Chargepack(Clone)") != null && Time.time >= m_CooldownTimestamp) {
            if (!GameObject.Find("Chargepack(Clone)").GetComponent<ExplosionControl>().HasExploded()) {
                GameObject.Find("Chargepack(Clone)").GetComponent<ExplosionControl>().Explode();
                m_CooldownTimestamp = Time.time + m_CurrentWeapon.GetFireRate();
            }
        }
        // Fire bullet
        else if (Input.GetButton("Fire1") && Time.time >= m_CooldownTimestamp && m_CurrentWeapon.GetCurrentAmmo() > 0) {
            FireBullet();
            m_CooldownTimestamp = Time.time + m_CurrentWeapon.GetFireRate();
        }
    }


    // Update is called once per frame
    void Update () {

        // Weapon cycling
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            CycleWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            CycleWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            CycleWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            CycleWeapon(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5)) {
            CycleWeapon(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6)) {
            CycleWeapon(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7)) {
            CycleWeapon(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8)) {
            CycleWeapon(7);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9)) {
            CycleWeapon(8);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0)) {
            CycleWeapon(9);
        }
        // Update HUD
        m_WeaponText.text = m_CurrentWeapon.GetName() + ": " + m_CurrentWeapon.GetCurrentAmmo();

    }

    private void CycleWeapon(int index) {
        if (m_Weapons[index].IsUnlocked()) {
            m_CurrentWeapon = m_Weapons[index];
            m_CooldownTimestamp = Time.time;
        }
    }

    private void FireBullet() {

        // Ammo logic
        m_CurrentWeapon.UpdateAmmo(m_CurrentWeapon.GetCurrentAmmo() - 1);


        // Bullet logic
        


        if (m_CurrentWeapon.IsProjectile()) {
            Rigidbody bullet = (Rigidbody)Instantiate(m_CurrentWeapon.GetPrefab(), m_BulletSpawn.transform.position, m_BulletSpawn.transform.rotation);
            bullet.velocity = m_BulletSpawn.transform.forward * m_CurrentWeapon.GetBulletSpeed();
            Destroy(bullet, 2.0f);
        } else {
            Rigidbody nonProjectile = (Rigidbody)Instantiate(m_CurrentWeapon.GetPrefab(), m_NonProjectileSpawn.transform.position, m_NonProjectileSpawn.transform.rotation);
        }
    }

}
