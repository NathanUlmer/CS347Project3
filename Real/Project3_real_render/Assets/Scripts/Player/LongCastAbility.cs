using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongCastAbility : MonoBehaviour
{
    //Ability Damage
    public int damage = 10;

    //Damage Vehicle On Collision
    void OnTriggerEnter(Collider other)
    {
        VehicleController vehicle = other.GetComponent<VehicleController>();
        if (vehicle != null)
        {
            vehicle.TakeDamage(damage);
        }
        //Destroy Slash On Collision
        Destroy(this.gameObject);
    }
}
