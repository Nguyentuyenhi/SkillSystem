using UnityEngine;

[CreateAssetMenu(fileName = "NewRank", menuName = "Cultivation/Rank Data")]
public class RankDataSO : ScriptableObject
{
    public string rankName;         // Tên: Đấu Khí Hậu Kỳ, Nhất Tinh Đấu Giả...
    public float dauUyCap;          // Giới hạn Đấu Uy (1000, 10000...)

    [Header("Liên kết")]
    public RankDataSO nextRank;     // Cảnh giới tiếp theo
    public bool requiresCondition;  // Có cần điều kiện đặc biệt (như 9 sao) không?
    public string conditionDescription; // Mô tả điều kiện (Vd: Cần giết Boss X)
}