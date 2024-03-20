using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    public List<GameObject> inventory;
    public Transform weaponRoot;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (inventory[0] != null)
            {
                EquipWeapon(0);
            }
        }
    }

    void EquipWeapon(int index)
    {
        //GameObject weapon = Instantiate(inventory[index], weaponRoot.position, Quaternion.identity, weaponRoot);
        GameObject weapon = Instantiate(inventory[index], weaponRoot);

    }
}
