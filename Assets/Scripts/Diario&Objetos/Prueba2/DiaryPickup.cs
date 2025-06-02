using UnityEngine;

public class DiaryPickup : MonoBehaviour
{
    public DiaryPageSO pageData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (pageData == null || DiarySystem.instance == null) return;

            DiarySystem.instance.AddPage(pageData);

            BookDiaryUI diaryUI = FindObjectOfType<BookDiaryUI>();
            if (diaryUI != null)
                diaryUI.OpenDiary();

            Destroy(gameObject);
        }
    }
}
