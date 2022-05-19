using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableArmature : MonoBehaviour
{

    public int currentHealth = 3;



    public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if(currentHealth <= 0){
            gameObject.SetActive(false);
        }
    }

}
