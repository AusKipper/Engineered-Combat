using UnityEngine;
using System.Collections;

public class ApplicationManager : MonoBehaviour 
{
	[SerializeField] private GameObject _persistentObject;
	[SerializeField] private ProfileManager _profileManager;
	[SerializeField] private MissionManager _missionManager;
	[SerializeField] private UIManager _UIManager;
	[SerializeField] private WeaponEditor _weaponEditor;

	private void Awake ()
	{
		DontDestroyOnLoad(_persistentObject);
		_UIManager.GoToProfileScreen();
	}

	public void LoadProfile (int index)
	{
		_profileManager.LoadProfile(index);
		_weaponEditor.OnProfileLoaded();
		GoHome();
	}
	
	public void LoadMission (int index)
	{
		_missionManager.LoadMission(index);
		_UIManager.GoToBriefingScreen();
	}
	
	public void StartMission ()
	{
		_UIManager.GoToMissionHud();
	}
	
	public void CompleteMission ()
	{
		_UIManager.GoToDebreifScreen();
		_profileManager.SubmitHighScore(_missionManager.GetTotalScore());
	}

	public void GoHome ()
	{
		_missionManager.GoHome();
		_UIManager.GoToHomeHud();
	}

	public void RestartMission ()
	{
		LoadMission(MissionManager.GetCurrentMissionIndex());// Loaded level index -2 is a mission index. Kinda confusing.
	}

	public void GoToProfileScreen ()
	{
		_UIManager.GoToProfileScreen();
	}

	public void Quit ()
	{
		Application.Quit();
	}
}


