using System.Collections.Generic;
using UnityEngine;

public class DiarySystem : MonoBehaviour
{
    public static DiarySystem instance;

    private Dictionary<int, DiaryPageSO> collectedPages = new();
    public int totalPages = 5;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddPage(DiaryPageSO page)
    {
        if (!collectedPages.ContainsKey(page.pageIndex))
        {
            collectedPages[page.pageIndex] = page;
            Debug.Log("Página recogida: " + page.title);
        }
    }

    public DiaryPageSO GetPage(int index)
    {
        return collectedPages.ContainsKey(index) ? collectedPages[index] : null;
    }

    public int GetTotalPages() => totalPages;
}
