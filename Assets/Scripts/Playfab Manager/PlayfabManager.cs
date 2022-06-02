using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;

public class PlayfabManager : MonoBehaviour
{
    public GameObject LeaderboardElementPrefab;
    [SerializeField] private Transform _leaderboardParentTransform;

    private void Start()
    {
        UIDLogin();
    }

    private void Login()
    {

    }

    private void UIDLogin()
    {
        LoginWithCustomIDRequest request = new LoginWithCustomIDRequest()
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log($"Login success with {result.PlayFabId}");
    }

    private void OnFailure(PlayFabError playfabError)
    {
        Debug.Log($"Failed request");
        Debug.Log($"{playfabError.GenerateErrorReport()}");
    }

    public void SendToLeaderboard(int score)
    {
        var leaderboardEntryRequest = new UpdatePlayerStatisticsRequest()
        {
            Statistics = new List<StatisticUpdate>()
            {
                new StatisticUpdate()
                {
                    StatisticName = "RushScore",
                    Value = score
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(leaderboardEntryRequest, OnLeaderboardEntrySuccess, OnFailure);
    }

    private void OnLeaderboardEntrySuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log($"Leaderboard entry sent successfully!");
    }

    public void GetLeaderboard()
    {
        var getLeaderboardRequest = new GetLeaderboardRequest()
        {
            StatisticName = "RushScore",
            StartPosition = 0,
            MaxResultsCount = 10
        };

        PlayFabClientAPI.GetLeaderboard(getLeaderboardRequest, OnLeaderboardRetrievalSuccess, OnFailure);
    }

    private void OnLeaderboardRetrievalSuccess(GetLeaderboardResult result)
    {
        foreach (var playerLeaderboardEntry in result.Leaderboard)
        {
            var leaderboardElement = Instantiate(LeaderboardElementPrefab, _leaderboardParentTransform);
            leaderboardElement.GetComponent<LeaderboardElement>().SetElements(playerLeaderboardEntry.Position + 1, playerLeaderboardEntry.PlayFabId, playerLeaderboardEntry.StatValue);
            Debug.Log($"Pos: {playerLeaderboardEntry.Position} - PlayfabID: {playerLeaderboardEntry.PlayFabId} - Score: {playerLeaderboardEntry.StatValue}");
        }
    }
}
