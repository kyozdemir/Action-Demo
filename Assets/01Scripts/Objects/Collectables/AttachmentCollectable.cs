using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    public class AttachmentCollectable : CollectableBase 
    {
        [SerializeField] private AttachmentSO attachmentSO;
        private PoolParticleObject _currentParticleObject;
        public override ScriptableObject CollectableData { get => attachmentSO; set => attachmentSO = (AttachmentSO)value; }

        public override void Collect(PlayerController player)
        {
            if (player.CanAttach(attachmentSO.AttachmentModel))
            {
                player.AttachCurrentWeapon(attachmentSO.AttachmentModel);
                gameObject.SetActive(false);
            }
        }

        public override void Setup()
        {
            _currentParticleObject = PoolManager.Instance.GetObject<PoolParticleObject>(
                attachmentSO.AttachmentModel.Name, 
                Vector3.zero, 
                Vector3.right * -90, 
                transform
            );
        }

        public override void OnDestroy()
        {
            PoolManager.Instance.ReturnObject<PoolParticleObject>(attachmentSO.AttachmentModel.Name, _currentParticleObject);
            _currentParticleObject = null;
        }

    }
}
