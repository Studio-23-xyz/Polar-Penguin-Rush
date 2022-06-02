using UnityEngine;

public class GameStateLeaderboard : GameState
{
    public GameObject LeaderboardUI;

    public override void Construct()
    {
        LeaderboardUI.SetActive(true);
        brain.PlayfabManagerScript.GetLeaderboard();
    }

    public override void Destruct()
    {
        LeaderboardUI.SetActive(false);
    }

    public void OnBlackClick()
    {
        brain.ChangeState(GetComponent<GameStateInit>());
    }
}
