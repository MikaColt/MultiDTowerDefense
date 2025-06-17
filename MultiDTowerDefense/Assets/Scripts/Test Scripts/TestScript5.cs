using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript5 : MonoBehaviour
{
    public GameObject BaseTower;
    public MainBase BaseMain {get{ return BaseTower.GetComponent<MainBase>(); }}

    // Start is called before the first frame update
    void Start()
    {
        BaseMain.AddAndEquipWeapon(new TestMachineGun());

        AttackSpeed = Weapon.BaseAttackSpeed;
    }

    // Update is called once per frame


    public float attackTimer = 0f;
    public WeaponClass Weapon { get { return BaseMain.EquippedWeapon; } }
    public Attack Shoot { get { return Weapon.AttackStats; } }
    public float AttackSpeed = 10f;
    void Update()
    {
        attackTimer += 1;
        if (attackTimer > AttackSpeed)
        {
            Debug.Log($"Attacking with {Weapon.Name}");
            attackTimer = 0;
        }
    }
}
