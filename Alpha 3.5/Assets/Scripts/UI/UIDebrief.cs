using UnityEngine;
using UnityEngine.UI;

public class UIDebrief : MonoBehaviour 
{
	[SerializeField] private MissionManager _missionManager;
	[SerializeField] private ProfileManager _profileManager;
	[SerializeField] private Text _objectiveScoreText;
	[SerializeField] private Text _colateralDamageText;
	[SerializeField] private Text _bulletCostPenaltyText;
	[SerializeField] private Text _timeScorePenaltyText;
	[SerializeField] private Text _totalScoreText;
	[SerializeField] private Text _messageText;
	[SerializeField] private Text _lastHighScoreText;
	[SerializeField] private Text _netWorthText;

	private void OnEnable ()
	{
		int lastScore = _profileManager.GetHighScore(Application.loadedLevel - 2);
		int score = _missionManager.GetTotalScore();
		if (score > lastScore)
		{
			_messageText.text = "New high score!";
			_netWorthText.text = "+" + (score - lastScore);
		}
		else
		{
			_messageText.text = "You can do better";
			_netWorthText.text = "+0";
		}
		_objectiveScoreText.text = "" + _missionManager.GetCurrentScore();
		_colateralDamageText.text = "-" + _missionManager.GetCollateralDamage();
		_bulletCostPenaltyText.text = "-" + _missionManager.GetBulletCost();
		_timeScorePenaltyText.text = "-" + _missionManager.GetTimeScorePenalty();
		_lastHighScoreText.text = lastScore.ToString();
		_totalScoreText.text = score.ToString();
	}
}
