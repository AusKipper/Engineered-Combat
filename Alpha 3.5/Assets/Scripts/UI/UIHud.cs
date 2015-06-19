using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHud : MonoBehaviour 
{
	[SerializeField] private Weapon _weapon;
	[SerializeField] private MissionManager _missionManager;
	[SerializeField] private GameObject _jamIndicatorObject;
	[SerializeField] private GameObject _overheatIndicatorObject;
	[SerializeField] private Slider _heatSlider;
	[SerializeField] private Slider _jamSlider;
	[SerializeField] private Slider _overheatSlider;
	[SerializeField] private Text _scoreText;
	[SerializeField] private Text _ammoWeaponText;
	[SerializeField] private Text _ammoInventoryText;

	private bool _jammed;
	private bool _overheated;

	private void Start () 
	{
		_heatSlider.maxValue = _weapon.GetHeatCapacity();
	}

	private void Update () 
	{
		_scoreText.text = _missionManager.GetTotalScore().ToString();
		_heatSlider.value = _weapon.GetHeatValue();
		_ammoWeaponText.text = _weapon.GetAmmoCountWeapon().ToString();
		_ammoInventoryText.text = _weapon.GetAmmoCountInventory().ToString();
		if (_jammed)
		{
			_jamSlider.value += Time.deltaTime;
			if (_jamSlider.value >= _jamSlider.maxValue)
			{
				Unjam();
			}
		}
		if (_overheated)
		{
			_overheatSlider.value += Time.deltaTime;
			if (_overheatSlider.value >= _overheatSlider.maxValue)
			{
				RemoveOverheat();
			}
		}
	}

	public void OnWeaponJam (float duration)
	{
		_jamSlider.value = 0;
		_jamSlider.maxValue = duration;
		_jamIndicatorObject.SetActive(true);
		_jammed = true;
	}

	public void OnWeaponOverheat (float duration)
	{	
		_overheatSlider.value = 0;
		_overheatSlider.maxValue = duration;
		_overheatIndicatorObject.SetActive(true);
		_overheated = true;
	}
	
	public void Reset ()
	{
		Unjam();
		RemoveOverheat();
	}

	private void Unjam ()
	{
		_jamIndicatorObject.SetActive(false);
		_jammed = false;
	}

	private void RemoveOverheat ()
	{
		_overheatIndicatorObject.SetActive(false);
		_overheated = false;
	}
}
