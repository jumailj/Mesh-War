using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using System;

public class PlayfabManager : MonoBehaviour
{

    public GameObject panelMainMenu;
    public GameObject panelStartGame;
    public GameObject panelScoreBoard;
    public GameObject buttonStartGame;
    public GameObject inputFieldName;

    public GameManager gamerManger;

    public Transform entryContainer;
    public Transform entryTemplate;

    // Start is called before the first frame update
    void Start()
    {
        Login();
    } 

    // Update is called once per frame
    void Login()
    {
        var request = new LoginWithCustomIDRequest {
            CustomId = Guid.NewGuid().ToString(),
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    }


    void OnLoginSuccess(LoginResult result) 
    {
        GetScoreboard();
    }

    public void SubmitButtonStartGame() {
        var inputName = inputFieldName.GetComponent<TMP_InputField>().text; 

        if (inputName == "")
            inputName = "Guest";

        var request = new UpdateUserTitleDisplayNameRequest {
            DisplayName = inputName,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);

        StartCoroutine( gamerManger.StartIntro());
         
    }


    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result) {
        Debug.Log("Updated display name");
        panelMainMenu.SetActive(false);
    }

    void OnError(PlayFabError error) { 
        Debug.Log("Error while logging in/creating account!" + error);

    }

    public void SendScoreBoard(int score) {
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName = "Score",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnScoreBoardUpdate, OnError);
    }

    void OnScoreBoardUpdate(UpdatePlayerStatisticsResult result) {
        Debug.Log("Success on score board update"); 
    }

    public void GetScoreboard() {
        var request = new GetLeaderboardRequest
            {
                StatisticName = "Score",
            };
            PlayFabClientAPI.GetLeaderboard(request, OnScoreBoardGet, OnError);
    }
    
    void OnScoreBoardGet(GetLeaderboardResult result) {
        float templateHeight = 30f;
        foreach (var item in result.Leaderboard) {
            Debug.Log(item.Position + " " + item.DisplayName + " " + item.StatValue);

            Transform entryTransform = Instantiate(entryTemplate, entryContainer);    
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * item.Position);
            entryTransform.Find("LPlayer").GetComponent<TMP_Text>().text = item.DisplayName;
            entryTransform.Find("LScore").GetComponent<TMP_Text>().text = item.StatValue.ToString();

        }
    }
}
