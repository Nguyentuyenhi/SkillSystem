using UnityEngine;
using UnityEngine.EventSystems;

public class SkillSlotDropHandler : MonoBehaviour, IDropHandler
{
    public SkillSlot slot;

    public void OnDrop(PointerEventData eventData)
    {
        var drag = eventData.pointerDrag.GetComponent<SkillDragHandler>();
        if (drag != null && slot != null)
        {
            slot.SetSkill(drag.skillData);
        }
    }
}
