using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [Tooltip("Damage that this object inflicts on it's target.")]
    [SerializeField] private int damage = 10;

    public int GetDamage()
    {
        return damage;
    }
}
