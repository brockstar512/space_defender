using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    [SerializeField] int damage = 100;

    //public bc we are going to be refeerncing this other places and int because we want to return the damage
    public int GetDamage()
    {
        return damage;
    }
    //when you call this. do somehting
    public void hit()
    {
        Destroy(gameObject);
    }


}
