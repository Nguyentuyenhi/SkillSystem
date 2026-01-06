using UnityEngine;

public class GameEnums : MonoBehaviour
{
    public enum StatType
    {
        SinhLuc,        // HP
        DauKhi,         // Mana
        TanCong,        // Attack
        PhongThu,       // Def
        TiLeBaoKich,    // Crit Rate
        PhaGiap,        // Armor Pen
        LinhHonLuc,     // Soul Power
        DiHoaChiLuc,    // Strange Flame Dmg
        KhangHoa,       // Fire Resist
        TocDoPhiHanh,   // Move Speed
        DinhLuc,        // Tenacity (CC resist)
        UyAp,           // Pressure
        LuyenDuoc       // Alchemy
    }
}
