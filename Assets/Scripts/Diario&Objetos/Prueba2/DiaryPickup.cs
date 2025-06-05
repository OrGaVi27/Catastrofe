using UnityEngine;

public class DiaryPickup : MonoBehaviour
{
    public DiaryPageSO pageData;
    [SerializeField] public AudioClip PickUpSound;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (pageData == null || DiarySystem.instance == null) return;

            DiarySystem.instance.AddPage(pageData);

            BookDiaryUI diaryUI = FindObjectOfType<BookDiaryUI>();
            if (diaryUI != null)
                diaryUI.OpenDiary();
                
            ControladorSonido.Instance.EjecutarSonido(PickUpSound);
            Destroy(gameObject);
        }
    }
}
