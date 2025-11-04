using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Game/Skill Data", order = 0)]
public class SkillData : ScriptableObject
{
    [Header("Thông tin c? b?n")]
    public string skillName;
    public Sprite icon;
    [TextArea] public string description;

    [Header("Thông s?")]
    public float cooldown = 2f;
    public float manaCost = 10f;
    public float damage = 20f;

    [Header("Thi?t l?p")]
    public KeyCode defaultHotkey = KeyCode.None;
    public GameObject skillEffectPrefab;

    [Header("Âm thanh & ho?t ?nh")]
    public AudioClip castSound;
    public AnimationClip castAnimation;

    public virtual void Activate(GameObject caster)
    {
        Debug.Log($"{caster.name} s? d?ng k? n?ng: {skillName}");
        if (skillEffectPrefab)
            Object.Instantiate(skillEffectPrefab, caster.transform.position + caster.transform.forward, Quaternion.identity);
    }
}
