using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SavePlayersName : MonoBehaviour
{
    private TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        inputField = transform.Find("inputField").GetComponent<TMP_InputField>();
        
    }

    public void CreateNewScore()
    {
        GlobalVariables.playerName = inputField.text;
        ScreenManager.Instance.ShowScoreBoard();
    }
}
