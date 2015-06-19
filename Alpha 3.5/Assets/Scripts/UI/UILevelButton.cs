using UnityEngine;
using UnityEngine.UI;

public class UILevelButton : MonoBehaviour 
{
	[SerializeField] private ProfileManager _profileManager;
	[SerializeField] private ApplicationManager _applicationManager;
	[SerializeField] private Text _scoreText;
	[SerializeField] private Text _levelNameText;

	private int _index;

	private void OnEnable ()
	{
		_scoreText.text = "" + _profileManager.GetHighScore(_index);
		_levelNameText.text = "Level " + _index;
	}

	public void OnClick ()
	{
		_applicationManager.LoadMission(_index);
	}

	public void SetIndex (int index)
	{
		_index = index;
		OnEnable();
	}
}