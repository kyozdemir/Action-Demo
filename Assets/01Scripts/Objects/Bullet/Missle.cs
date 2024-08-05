using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    public class Missle : Bullet, IExplodable
    {
        //I want to use composition here.
        //Not every explodable deals damage. Maybe you have sonar grenade that shows enemies behind walls after explosion.
        [SerializeField] private ExplodeOnImpact explodeOnImpact;

        public void Explode()
        {
            Collider[] affectedColliders = explodeOnImpact.GetAffectedCollidersByExplosion(areaOfEffect);
            foreach (Collider item in affectedColliders)
            {
                damageDealtConfigs.Add(new DamageDealtConfig(DamageTypes.Explosion, item));
            }
        }

        protected override void Impacted(Collider other)
        {
            Explode();
            base.Impacted(other);
        }
    }
}
