using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    [CreateAssetMenu(fileName = "AttachmentSO", menuName = "Attachment", order = 1)]
    public class AttachmentSO : ScriptableObject
    {
        public AttachmentModel AttachmentModel;
        public PoolParticleObject AttachmentParticle;
    }

    [System.Serializable]
    public struct AttachmentModel
    {
        public string Name;
        public AttachmentType AttachmentType;
        public DamageAttributes DamageAttributes;
    }
}
