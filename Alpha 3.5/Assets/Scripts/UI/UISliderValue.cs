using UnityEngine;
using UnityEngine.UI;

public class UISliderValue : MonoBehaviour 
{
	public Slider _slider;
	public Text _label;

	public void Awake ()
	{
		UpdateText();
	}

	public void Update () 
	{
		UpdateText();
	}

	public void UpdateText ()
	{
		string newText = "" + _slider.value;
		if (_label.text != newText)
		{
			_label.text = newText;
		}
	}
}
