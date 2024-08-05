using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ActionDemo
{
    public class DamageNumbers : PoolObject
    {
        [SerializeField] private TMP_Text armorText, healthText;
        public override void ResetObject(){}

        public void SetValues(float armorDamage, float healthDamage)
        {
            armorText.SetText($"-{armorDamage}");
            healthText.SetText($"-{healthDamage}");
        }

        public void PlayAnimation()
        {
            Vector2 randomCircle = Random.insideUnitCircle * .5f;
            transform.position = transform.position + new Vector3(randomCircle.x, randomCircle.y, 0f);
            transform.DOScale(1f, .25f).From(0).SetLoops(2, LoopType.Yoyo)
            .OnComplete(() => PoolManager.Instance.ReturnObject<DamageNumbers>(Constants.PoolKeys.DAMAGE_NUMBERS, this));
        }

    }
}
