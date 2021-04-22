using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongCastAbility : MonoBehaviour
{
    public int damage = 10;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vehicle")
        {
            VehicleController vehicle = other.GetComponent<VehicleController>();
            if (vehicle != null)
            {
                vehicle.TakeDamage(damage);
            }
            Destroy(this.gameObject);
        }
    }
}
