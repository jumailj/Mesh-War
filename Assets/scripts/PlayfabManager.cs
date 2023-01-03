using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Login();
    }

    // Update is called once per frame
    void Login()
    {
        var request = new LoginWithCustomIDRequest {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result) {
        Debug.Log("Success login/account create!");
    }

    void OnError(PlayFabError error) { 
        Debug.Log("Error while logging in/creating account!");
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

    public void GetScoreBoard() {
        var request = new GetLeaderboardRequest
            {
                StatisticName = "Score",
            };
            PlayFabClientAPI.GetLeaderboard(request, OnScoreBoardGet, OnError);
    }
    
    void OnScoreBoardGet(GetLeaderboardResult result) {
        foreach (var item in result.Leaderboard) {
                Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }
}
