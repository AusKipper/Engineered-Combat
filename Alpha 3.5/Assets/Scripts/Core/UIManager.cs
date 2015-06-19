using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
	[SerializeField] private GameObject _mainMenu;
	[SerializeField] private GameObject _weaponEditor;
	[SerializeField] private GameObject _briefingScreen;
	[SerializeField] private GameObject _debriefScreen;
	[SerializeField] private GameObject _profileScreen;
	[SerializeField] private GameObject _hud;
	[SerializeField] private GameObject _score;

	private Location _location;

	private void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (_location == Location.Home || _location == Location.Mission || _location == Location.WeaponEditor)
			{
				GoToMainMenu();
			}
			else if (_location == Location.MainMenuHome)
			{
				GoToHomeHud();
			}
			else if (_location == Location.MainMenuMission)
			{
				GoToMissionHud();
			}
		}
		else if (Input.GetKeyDown(KeyCode.BackQuote))
		{
			if (_location == Location.Home || _location == Location.MainMenuHome)
			{
				GoToWeaponEditor();
			}
			else if (_location == Location.WeaponEditor)
			{
				GoToHomeHud();
			}
		}
	}

	private void OnApplicationFocus ()
	{
		if (_location == Location.Home || _location == Location.Mission)
		{
			EnterGameMode();
		}
	}

	public void GoToMainMenu ()
	{
		EnableMenu(_mainMenu);
		if (_location == Location.Home || _location == Location.WeaponEditor)
		{
			_location = Location.MainMenuHome;
		}
		else if (_location == Location.Mission)
		{
			_location = Location.MainMenuMission;
		}
	}

	public void GoToWeaponEditor ()
	{
		EnableMenu(_weaponEditor);
		_location = Location.WeaponEditor;
	}

	public void GoToHomeHud ()
	{
		EnableMenu(_hud);
		EnterGameMode();
		_score.SetActive(false);
		_location = Location.Home;
	}

	public void GoToMissionHud ()
	{
		EnableMenu(_hud);
		EnterGameMode();
		_score.SetActive(true);
		_location = Location.Mission;
	}

	public void GoToBriefingScreen ()
	{
		EnableMenu(_briefingScreen);
		_location = Location.Briefing;
	}

	public void GoToDebreifScreen ()
	{
		EnableMenu(_debriefScreen);
		_location = Location.Debriefing;
	}
	
	public void GoToProfileScreen ()
	{
		EnableMenu(_profileScreen);
		_location = Location.ProfileScreen;
	}

	public void DisableAllMenus ()
	{
		_profileScreen.SetActive(false);
		_mainMenu.SetActive(false);
		_weaponEditor.SetActive(false);
		_hud.SetActive(false);
		_briefingScreen.SetActive(false);
		_debriefScreen.SetActive(false);
	}

	public void EnableMenu (GameObject menu)
	{
		DisableAllMenus();
		EnterMenuMode();
		menu.SetActive(true);
	}

	private void EnterGameMode ()
	{
		Screen.showCursor = false;
		Screen.lockCursor = true;
		Time.timeScale = 1f;
	}

	private void EnterMenuMode ()
	{
		Screen.showCursor = true;
		Screen.lockCursor = false;
		Time.timeScale = 0f;
	}

	private enum Location
	{
		ProfileScreen,
		Home,
		Mission,
		MainMenuHome,
		MainMenuMission,
		WeaponEditor,
		Briefing,
		Debriefing
	}
}
