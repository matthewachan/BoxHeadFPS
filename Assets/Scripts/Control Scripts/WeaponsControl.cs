using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsControl : MonoBehaviour {
    [SerializeField]
    private Rigidbody m_BulletPrefab;
    [SerializeField]
    private float m_BulletSpeed;

    private Weapon[] m_Weapons;
    private Weapon m_CurrentWeapon;
    private UnityEngine.UI.Text m_WeaponText;
    private float m_CooldownTimestamp;


    // Use this for initialization
    void Start () {
        
        // Create list of weapons
        m_Weapons = new Weapon[2];
        m_Weapons[0] = new global::Weapon("Pistol", true, 50, 50, 10, .5f);
        m_Weapons[1] = new global::Weapon("UZI", true, 100, 100, 10, .1f);

        // Setup HUD
        m_CurrentWeapon = m_Weapons[0];
        m_CooldownTimestamp = Time.time + m_CurrentWeapon.GetFireRate();
        m_WeaponText = GameObject.Find("Weapon").GetComponent<UnityEngine.UI.Text>();
        m_WeaponText.text = m_CurrentWeapon.GetName() + ": " + m_CurrentWeapon.GetCurrentAmmo();
    }

    public void UpdateInventory(string name) {
        for ( int i = 0; i < m_Weapons.Length; ++i) {
            if (m_Weapons[i].GetName() == name)
                m_Weapons[i].UpdateAmmo(m_Weapons[i].GetMaxAmmo());
        }
    }

    public Weapon GetCurrentWeapon() {
        return m_CurrentWeapon;
    }

    private void FixedUpdate() {
        // Fire bullet
        if (Input.GetButton("Fire1") && Time.time >= m_CooldownTimestamp && m_CurrentWeapon.GetCurrentAmmo() > 0) {
            FireBullet();
            m_CooldownTimestamp = Time.time + m_CurrentWeapon.GetFireRate();
        }
    }


    // Update is called once per frame
    void Update () {

        // Weapon cycling
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            if (m_Weapons[0].IsUnlocked())
                m_CurrentWeapon = m_Weapons[0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            if (m_Weapons[1].IsUnlocked()) 
                m_CurrentWeapon = m_Weapons[1];
        }

        // Update HUD
        m_WeaponText.text = m_CurrentWeapon.GetName() + ": " + m_CurrentWeapon.GetCurrentAmmo();

    }

    private void FireBullet() {

        // Ammo logic
        m_CurrentWeapon.UpdateAmmo(m_CurrentWeapon.GetCurrentAmmo() - 1);


        // Bullet logic
        GameObject spawn = GameObject.Find("BulletSpawn");
        Rigidbody bullet = (Rigidbody)Instantiate(m_BulletPrefab, spawn.transform.position, spawn.transform.rotation);
        bullet.velocity = spawn.transform.forward * m_BulletSpeed;
        Destroy(bullet, 2.0f);
    }

}
