using System.Collections.Generic;
using UnityEngine;
using static GameEnums;

[CreateAssetMenu(fileName = "StatCostConfig", menuName = "Cultivation/Stat Cost Config")]
public class StatCostConfigSO : ScriptableObject
{
    [System.Serializable]
    public struct StatCostData
    {
        public StatType type;
        public float baseValue;      // Giá trị cộng thêm mỗi lần nâng (VD: +5 Máu)
        public int baseCost;         // Giá gốc (VD: 10 điểm)
        public float costMultiplier; // Hệ số tăng giá (VD: 1.2 là tăng 20% mỗi cấp)
        public int costIncrement;    // Hoặc cộng thẳng (VD: +5 giá mỗi cấp)
    }

    public List<StatCostData> costList;

    public StatCostData GetData(StatType type) => costList.Find(x => x.type == type);
}