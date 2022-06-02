using TMPro;
using UnityEngine;

public class LeaderboardElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _positionText;
    [SerializeField] private TextMeshProUGUI _playerIdText;
    [SerializeField] private TextMeshProUGUI _playerScoreText;

    public void SetElements(int position, string playerID, int score)
    {
        _positionText.text = position.ToString();
        _playerIdText.text = playerID;
        _playerScoreText.text = score.ToString();
    }
}
