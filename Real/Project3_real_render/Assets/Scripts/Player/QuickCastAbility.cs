using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickCastAbility : MonoBehaviour
{
    //Ability Damage
    public int damage = 5;

    //Damage Vehicle On Collision
    void OnTriggerEnter(Collider other)
    {
        VehicleController vehicle = other.GetComponent<VehicleController>();
        if (vehicle != null)
        {
            vehicle.TakeDamage(damage);
        }
        //Destroy Slash On Collision
        if (other.tag == "Vehicle")
        {
            Destroy(this.gameObject);
        }
    }
}
