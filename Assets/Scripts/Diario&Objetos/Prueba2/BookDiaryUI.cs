using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookDiaryUI : MonoBehaviour
{
    public GameObject diaryPanel;
    public TextMeshProUGUI pageNumberText;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI contentText;
    public Image brokenPageImage;
    public GameObject textContentGroup;

    private int currentPage = 0;

    void Start()
    {
        ShowPage(currentPage);
    }

    public void ShowPage(int index)
    {
        currentPage = index;
        pageNumberText.text = $"Pàgina {index + 1} de {DiarySystem.instance.GetTotalPages()}";

        var page = DiarySystem.instance.GetPage(index);

        if (page != null)
        {
            titleText.text = page.title;
            contentText.text = page.content;
            textContentGroup.SetActive(true);
            brokenPageImage.gameObject.SetActive(false);
        }
        else
        {
            titleText.text = "";
            contentText.text = "";
            textContentGroup.SetActive(false);
            brokenPageImage.gameObject.SetActive(true);
        }
    }

    public void NextPage()
    {
        if (currentPage < DiarySystem.instance.GetTotalPages() - 1)
            ShowPage(currentPage + 1);
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
            ShowPage(currentPage - 1);
    }

    public void OpenDiary()
    {
        diaryPanel.SetActive(true);
        ShowPage(currentPage);
    }

    public void CloseDiary()
    {
        diaryPanel.SetActive(false);
    }
}
