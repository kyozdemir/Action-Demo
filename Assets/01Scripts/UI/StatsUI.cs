using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ActionDemo
{
    public class StatsUI : MonoBehaviour
    {
        [SerializeField] private List<StatsModel> statsModels;

        public void DisplayUI(bool canDisplay)
        {
            gameObject.SetActive(canDisplay);
        }

        public void ResetUI()
        {
            statsModels.ForEach(x => x.StatsBar.fillAmount = 1);
        }

        public void UpdateStat(StatsType statsType, float current, float max)
        {
            statsModels.FirstOrDefault(x => x.StatsType == statsType).StatsBar.fillAmount = current / max;
        }
    }

    [System.Serializable]
    public struct StatsModel
    {
        public StatsType StatsType;
        public Image StatsBar;
    }
}
