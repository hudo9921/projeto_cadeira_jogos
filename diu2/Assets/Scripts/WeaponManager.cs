using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public Image weaponIconImage; // Referência ao componente UI Image para mostrar o ícone da arma

    public Weapon currentWeapon; // Arma atual do player
    public AmmunitionBarBehavior ammunitionBar;

    public PlayerBehavior playerBehavior;

    public enum WeaponID
    {
        Pistol,
        Rifle
    }

    // Classe para representar uma arma
    [System.Serializable]
    public class Weapon
    {
        public WeaponID weaponID;  // Tipo de arma (enum)
        public Sprite weaponIcon; // Ícone da arma
    }

    public Weapon[] weapons; // Lista das armas disponíveis
    private int currentWeaponIndex = 0;

    void Start()
    {
        EquipWeapon(currentWeaponIndex);
    }

    // Método para equipar uma arma
    public void EquipWeapon(int weaponIndex)
    {
        currentWeapon = weapons[weaponIndex];
        UpdateWeaponDisplay();
    }

    // Método para atualizar o display da arma
    void UpdateWeaponDisplay()
    {
        if (weaponIconImage != null)
        {
            weaponIconImage.sprite = currentWeapon.weaponIcon;
        }
    }

    // Exemplo de como trocar a arma usando teclas de atalho
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(0);
            ammunitionBar.SetAmmunition(playerBehavior.maxMunition);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(1); // Equipar a segunda arma
            ammunitionBar.SetAmmunition(playerBehavior.munition);
        }
    }
}
