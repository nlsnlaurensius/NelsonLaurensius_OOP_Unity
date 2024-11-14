using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weaponHolder; 
    Weapon weapon; 
    
    void Awake()
    {
        if(weaponHolder != null)
        {
            weapon = Instantiate(weaponHolder);
            TurnVisual(false, weapon);
        }
        else
        {
            Debug.LogError("WeaponPickup Awake: Gak ada weapon holder.");   
        }
    }

    void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Weapon existingWeapon = other.GetComponentInChildren<Weapon>();
            if (existingWeapon != null)
            {
                existingWeapon.gameObject.SetActive(false);
            }

            if (weapon != null)
            {
                weapon.transform.SetParent(other.transform);
                weapon.transform.localPosition = new Vector3(0, 0, 1);

                TurnVisual(true);
            }
            else
            {
                Debug.LogWarning("Weapon ga ada di WeaponPickup.");
            }
        }
    }

    void TurnVisual(bool on)
    {
        if (weapon != null)
        {
            foreach (var component in weapon.GetComponentsInChildren<MonoBehaviour>())
            {
                component.enabled = on;
            }   
            weapon.gameObject.SetActive(on);
        }
    }

    void TurnVisual(bool on, Weapon weapon)
    {
        foreach (var component in weapon.GetComponentsInChildren<MonoBehaviour>())
        {
            component.enabled = on;
        }
        weapon.gameObject.SetActive(on);
    }
}
