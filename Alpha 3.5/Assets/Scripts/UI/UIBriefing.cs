using UnityEngine;
using UnityEngine.UI;

public class UIBriefing : MonoBehaviour 
{
	[SerializeField] private Text _briefingText;

	private void OnEnable ()
	{
		_briefingText.text = LevelData._current.GetBriefingText();
	}
}
