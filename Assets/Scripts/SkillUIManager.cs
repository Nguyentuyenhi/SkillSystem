using UnityEngine;
using UnityEngine.UI;

public class SkillUIManager : MonoBehaviour
{
    public Transform skillLibraryPanel; // Grid ch?a t?t c? skill có
    public Transform skillSlotPanel;    // 5 slot gán skill
    public GameObject skillIconPrefab;  // Prefab UI cho skill (có Image + SkillDragHandler)
    public SkillData[] allSkills;       // Gán toàn b? skill (ScriptableObject)

    void Start()
    {
        PopulateLibrary();
    }

    void PopulateLibrary()
    {
        foreach (Transform child in skillLibraryPanel)
            Destroy(child.gameObject);

        foreach (var skill in allSkills)
        {
            var icon = Instantiate(skillIconPrefab, skillLibraryPanel);
            var img = icon.GetComponent<Image>();
            var drag = icon.GetComponent<SkillDragHandler>();
            img.sprite = skill.icon;
            drag.skillData = skill;
        }
    }
}
