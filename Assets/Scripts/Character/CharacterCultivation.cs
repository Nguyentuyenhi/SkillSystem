using System.Collections.Generic;
using UnityEngine;
using static GameEnums;

public class CharacterCultivation : MonoBehaviour
{
    [Header("Cài đặt")]
    public RankDataSO currentRank;       // Cảnh giới hiện tại
    public StatCostConfigSO statConfig;  // Cấu hình giá tiền

    [Header("Tài nguyên (Runtime)")]
    [SerializeField] private float _dauUyTichLuy = 100; // Khởi đầu 100
    [SerializeField] private float _diemDauUy = 100;    // Khởi đầu 100

    // Lưu trữ cấp độ của từng chỉ số (để tính giá tiền lần sau)
    private Dictionary<StatType, int> _statLevels = new Dictionary<StatType, int>();
    // Lưu trữ giá trị thực của chỉ số
    private Dictionary<StatType, float> _statValues = new Dictionary<StatType, float>();

    // Sự kiện Update UI
    public System.Action OnResourcesChanged;
    public System.Action OnStatsChanged;

    private void Awake()
    {
        InitializeDefaultStats();
    }

    private void InitializeDefaultStats()
    {
        // Khởi tạo tất cả level chỉ số = 0
        foreach (StatType type in System.Enum.GetValues(typeof(StatType)))
        {
            _statLevels[type] = 0;
            _statValues[type] = 0;
        }

        // Cài đặt chỉ số cơ bản theo yêu cầu của bạn
        _statValues[StatType.SinhLuc] = 20;
        _statValues[StatType.DauKhi] = 20;
        _statValues[StatType.TanCong] = 1;
        _statValues[StatType.TiLeBaoKich] = 1; // 1%

        // Các chỉ số còn lại mặc định là 0
    }

    // ========================================================================
    // 1. CƠ CHẾ NHẬN THƯỞNG (CÓ CHECK CAP)
    // ========================================================================
    public void GainReward(float amount)
    {
        // Kiểm tra xem đã chạm trần của Cảnh giới hiện tại chưa
        if (_dauUyTichLuy >= currentRank.dauUyCap)
        {
            Debug.LogWarning("Đã đạt giới hạn cảnh giới! Cần Đột phá để nhận thêm Đấu Uy.");
            return; // Dừng lại ngay, không cộng gì cả
        }

        // Nếu chưa chạm trần, cộng cả 2
        _dauUyTichLuy += amount;
        _diemDauUy += amount;

        // Đảm bảo không vượt quá trần (Kẹp giá trị)
        if (_dauUyTichLuy > currentRank.dauUyCap)
        {
            float overflow = _dauUyTichLuy - currentRank.dauUyCap;
            _dauUyTichLuy = currentRank.dauUyCap;
            _diemDauUy -= overflow; // Trừ lại phần thừa của điểm tiêu xài (tùy chọn)
        }

        Debug.Log($"Nhận {amount} Đấu Uy. Tổng: {_dauUyTichLuy}/{currentRank.dauUyCap}");
        OnResourcesChanged?.Invoke();
    }

    // ========================================================================
    // 2. CƠ CHẾ NÂNG CẤP CHỈ SỐ (TĂNG GIÁ)
    // ========================================================================
    public void UpgradeStat(StatType type)
    {
        var config = statConfig.GetData(type);
        int currentLvl = _statLevels[type];

        // Công thức tính giá: Giá gốc + (Cấp hiện tại * Giá tăng thêm)
        // Ví dụ: Gốc 10, mỗi cấp tăng 5. Cấp 0 = 10, Cấp 1 = 15, Cấp 2 = 20...
        int cost = config.baseCost + (currentLvl * config.costIncrement);

        // Kiểm tra tiền
        if (_diemDauUy >= cost)
        {
            _diemDauUy -= cost;             // Trừ tiền
            _statLevels[type]++;            // Tăng cấp chỉ số
            _statValues[type] += config.baseValue; // Cộng chỉ số thực

            Debug.Log($"Nâng {type} lên cấp {_statLevels[type]}. Tốn {cost} điểm. Giá trị mới: {_statValues[type]}");

            OnResourcesChanged?.Invoke();
            OnStatsChanged?.Invoke();
        }
        else
        {
            Debug.Log($"Không đủ điểm! Cần {cost}, đang có {_diemDauUy}");
        }
    }

    // ========================================================================
    // 3. CƠ CHẾ ĐỘT PHÁ CẢNH GIỚI
    // ========================================================================
    public void AttemptBreakthrough()
    {
        // 1. Phải đạt trần Đấu Uy mới được đột phá
        if (_dauUyTichLuy < currentRank.dauUyCap)
        {
            Debug.Log("Chưa đủ Đấu Uy tích lũy để đột phá!");
            return;
        }

        // 2. Kiểm tra điều kiện riêng (cho 9 sao/Đỉnh phong)
        if (currentRank.requiresCondition)
        {
            // Code kiểm tra điều kiện (Item, Boss...) viết ở đây
            // if (!HasItem("DanDuoc_X")) return;
            Debug.Log($"Kiểm tra điều kiện: {currentRank.conditionDescription} -> OK");
        }

        // 3. Tiến cấp
        if (currentRank.nextRank != null)
        {
            currentRank = currentRank.nextRank;
            Debug.Log($"Đột phá thành công! Cảnh giới mới: {currentRank.rankName}. Giới hạn mới: {currentRank.dauUyCap}");

            // Mở khóa giới hạn, người chơi có thể farm tiếp
        }
        else
        {
            Debug.Log("Đã đạt đỉnh phong thiên hạ (Hết game/Chưa update)");
        }
    }

    // Hàm lấy chỉ số cho Combat System gọi
    public float GetStat(StatType type) => _statValues.ContainsKey(type) ? _statValues[type] : 0;
}