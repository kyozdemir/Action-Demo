using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionDemo
{
    public class AttachmentManager : MonoBehaviour
    {
        [SerializeField] private AttachmentSO[] attachments;

        void Start()
        {
            foreach (AttachmentSO item in attachments)
            {
                PoolManager.Instance.CreatePool<PoolParticleObject>(item.AttachmentModel.Name, item.AttachmentParticle, 3);
            }
        }
    }
}
