using UnityEngine;
using System.Collections;

public class MissionManager : MonoBehaviour 
{
	[SerializeField] private ApplicationManager _applicationManager;
	[SerializeField] private float _scorePenaltyPerBulletCost;
	[SerializeField] private float _scorePenaltyPerSecond;
	[SerializeField] private float _scorePenaltyPerCollateralDamage;

	private int _targetCount;
	private float _bulletCostScorePenalty;
	private float _timeScorePenalty;
	private float _collateralDamageScorePenalty;
	private float _objectiveScore;
	private float _totalScore;

	public static MissionManager _instance;

	private void Awake ()
	{
		_instance = this;
	}

	private void Update ()
	{
		_timeScorePenalty -= Time.deltaTime * _scorePenaltyPerSecond;
		UpdateTotalScore();
	}

	public void OnBulletFired (float cost)
	{
		_bulletCostScorePenalty += cost * _scorePenaltyPerBulletCost;
	}

	public void OnTargetDestroyed (int score)
	{
		_objectiveScore += score;
		UpdateTotalScore();
		_targetCount--;
		if (_targetCount == 0)
		{
			if (Application.loadedLevelName != "Home")
			{
				_applicationManager.CompleteMission();
			}
		}
	}

	public void OnCollateralDamage (float damage)
	{
		_collateralDamageScorePenalty += damage * _scorePenaltyPerCollateralDamage;
	}

	public void LoadMission (int index)
	{
		StartCoroutine("LoadMissionCoroutine", index + 2);
	}

	public void GoHome ()
	{
		Application.LoadLevel("Home");
	}

	private IEnumerator LoadMissionCoroutine (int index)
	{
		Application.LoadLevel(index);
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();

		_targetCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
		_objectiveScore = 0;
		_bulletCostScorePenalty = 0;
		_collateralDamageScorePenalty = 0;
		_timeScorePenalty = 0;
		_totalScore = 0;
	}

	private void UpdateTotalScore ()
	{
		_totalScore = (int)(_objectiveScore - _bulletCostScorePenalty - _timeScorePenalty - _collateralDamageScorePenalty);
	}

	public int GetCurrentScore ()
	{
		return (int)_objectiveScore;
	}

	public int GetTotalScore ()
	{
		return (int)_totalScore;
	}

	public int GetBulletCost ()
	{
		return (int)_bulletCostScorePenalty;
	}

	public int GetCollateralDamage ()
	{
		return (int)_collateralDamageScorePenalty;
	}

	public int GetTimeScorePenalty ()
	{
		return (int)_timeScorePenalty;
	}

	public static int GetCurrentMissionIndex ()
	{
		return Application.loadedLevel - 2;
	}
}