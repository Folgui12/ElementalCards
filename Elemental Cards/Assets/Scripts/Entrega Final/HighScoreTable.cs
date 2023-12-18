using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    public static HighScoreTable Instance;
    private Transform entryContainer;
    private Transform entryTemplate;
    private static List<HighscoreEntry> highscoreEntryList;
    private static List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        highscoreEntryList = new List<HighscoreEntry>();
        entryContainer = GameObject.FindGameObjectWithTag("highScoreEntryContainer").transform;
        entryTemplate = entryContainer.Find("highScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        HighscoreEntry newScore = new HighscoreEntry { score = GlobalVariables.enemiesKilled * 10, name = GlobalVariables.playerName };

        highscoreEntryList.Add(newScore);

        QuickSort(highscoreEntryList, 0, highscoreEntryList.Count - 1);

        Debug.Log(highscoreEntryList.Count);

        highscoreEntryTransformList = new List<Transform>();

        for (int i = highscoreEntryList.Count - 1; i >= 0; i--)
        {
            CreateHighscoreEntryTransform(highscoreEntryList[i], entryContainer, highscoreEntryTransformList);
        }

        DontDestroyOnLoad(gameObject);
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
    
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
}
