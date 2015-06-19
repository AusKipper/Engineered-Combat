using UnityEngine;
using UnityEngine.UI;

public class UIProfileSlot : MonoBehaviour 
{
	[SerializeField] private ApplicationManager _applicationManager;
	[SerializeField] private ProfileManager _profileManager;
	[SerializeField] private Text _netWorthText;
	[SerializeField] private Text _selectButtonText;
	[SerializeField] private int _index;

	private void OnEnable ()
	{
		if (_profileManager.ProfileExists(_index))
		{
			_selectButtonText.text = "Select";
			_netWorthText.text = "" + _profileManager.GetProfileNetWorth(_index);
		}
		else
		{
			_selectButtonText.text = "Create";
			_netWorthText.text = "...";
		}
	}

	public void OnDeleteButton ()
	{
		_profileManager.DeleteProfile(_index);
		OnEnable();
	}

	public void OnSelectButton ()
	{
		if (_profileManager.ProfileExists(_index))
		{
			_applicationManager.LoadProfile(_index);
		}
		else
		{
			_profileManager.CreateProfile(_index);
			_applicationManager.LoadProfile(_index);
		}
	}
}