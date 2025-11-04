using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public Image iconImage;
    public SkillData currentSkill;

    public void SetSkill(SkillData skill)
    {
        currentSkill = skill;
        if (iconImage != null)
        {
            iconImage.sprite = skill.icon;
            iconImage.enabled = true;
        }
    }

    public void ClearSlot()
    {
        currentSkill = null;
        if (iconImage != null)
            iconImage.enabled = false;
    }

    public void Use(GameObject caster)
    {
        if (currentSkill != null)
            currentSkill.Activate(caster);
    }
}
