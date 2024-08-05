using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    [CreateAssetMenu(menuName = "Particles/ParticleSO")]
    public class ParticleSO : ScriptableObject
    {
        public string Name;
        public PoolParticleObject PoolParticleObject;
    }
}
