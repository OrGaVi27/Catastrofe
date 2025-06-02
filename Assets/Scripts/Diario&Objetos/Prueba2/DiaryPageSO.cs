using UnityEngine;

[CreateAssetMenu(fileName = "New Diary Page", menuName = "Diary/Page")]
public class DiaryPageSO : ScriptableObject
{
    public int pageIndex; // De 0 a N-1
    public string title;
    [TextArea(5, 10)]
    public string content;
}
