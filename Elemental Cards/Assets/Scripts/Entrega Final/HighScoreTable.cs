using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        entryContainer = GameObject.FindGameObjectWithTag("highScoreEntryContainer").transform;
        entryTemplate = entryContainer.Find("highScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        QuickSort(GlobalVariables.highscoreEntryList, 0, GlobalVariables.highscoreEntryList.Count - 1);

        highscoreEntryTransformList = new List<Transform>();

        for (int i = GlobalVariables.highscoreEntryList.Count - 1; i >= 0; i--)
        {
            CreateHighscoreEntryTransform(GlobalVariables.highscoreEntryList[i], entryContainer, highscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry entry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 40f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;

        switch (rank)
        {
            case 1:
                rankString = "1ST";
                break;
            case 2:
                rankString = "2ST";
                break;
            case 3:
                rankString = "3ST";
                break;

            default:
                rankString = rank + "TH";
                break;
        }

        entryTransform.Find("posText").GetComponent<Text>().text = rankString;


        int score = entry.score;
        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

        string name = entry.name;
        entryTransform.Find("nameText").GetComponent<Text>().text = name;

        transformList.Add(entryTransform);
    }

    private void QuickSort(List<HighscoreEntry> entryList, int left, int right)
    {
        int pivot;

        if(left < right)
        {
            pivot = Partition(entryList, left, right);

            if(pivot > 1)
                QuickSort(entryList, left, pivot - 1);
            if(pivot + 1 < right)
                QuickSort(entryList, pivot + 1, right);
        }
    }

    private int Partition(List<HighscoreEntry> entryList, int left, int right)
    {
        int pivot;
        int aux = (left + right) / 2;
        pivot = entryList[aux].score;

        while (true)
        {
            while(entryList[left].score < pivot)
            {
                left++;
            }
            while (entryList[right].score > pivot)
            {
                right--;
            }
            if (left < right)
            {
                HighscoreEntry temp = entryList[right];
                entryList[right] = entryList[left];
                entryList[left] = temp;
            }
            else
                return right;
        }
    }
}
