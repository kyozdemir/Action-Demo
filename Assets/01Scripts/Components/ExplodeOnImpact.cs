using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    public class ExplodeOnImpact : MonoBehaviour
    {
        public Collider[] GetAffectedCollidersByExplosion(float areaOfEffect)
        {
            return Physics.OverlapSphere(
                transform.position,
                areaOfEffect
                );
        }
    }
}
