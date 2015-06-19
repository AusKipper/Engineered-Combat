using UnityEngine;
using UnityEngine.UI;

//Ideally this class needs a total revamp. Weapon setting should be a class in itself, 
//so we could just keep one array of settings and pass it around as an array, instead of current
//20 (+10 more needed) variables.
public class WeaponEditor : MonoBehaviour 
{
	[SerializeField] private ProfileManager _profileManager;
	[SerializeField] private Weapon _weapon;
	[SerializeField] private InputField _nameInput;
	[SerializeField] private Slider _ammoCapacityInventorySlider;	
	[SerializeField] private Slider _ammoCapacityMagazineSlider;	
	//[SerializeField] private Slider _projectilePowerSlider;
	[SerializeField] private Slider _recoilDampingSlider;
	//[SerializeField] private Slider _bulletForceSlider;
	[SerializeField] private Slider _reloadSpeedSlider;
	[SerializeField] private Slider _heatCoolingSlider;
	[SerializeField] private Slider _fireRateSlider;
	[SerializeField] private Slider _accuracySlider;
	[SerializeField] private Slider _reliabilitySlider;
	[SerializeField] private Toggle _fullAutoToggle;
	[SerializeField] private float _pricePerAmmoCapacityMagazineUnit;
	[SerializeField] private float _pricePerAmmoCapacityInventoryUnit;
	[SerializeField] private float _pricePerProjectilePowerUnit;
	[SerializeField] private float _pricePerRecoilDampingUnit;
	[SerializeField] private float _pricePerBulletForceUnit;
	[SerializeField] private float _pricePerReloadSpeedUnit;
	[SerializeField] private float _pricePerHeatCoolingUnit;
	[SerializeField] private float _pricePerReliabilityUnit;
	[SerializeField] private float _pricePerFierRateUnit;
	[SerializeField] private float _pricePerAccuracyUnit;
	[SerializeField] private float _priceForFullAuto;
	[SerializeField] private Text _netWorthLabel;
	[SerializeField] private Text _priceLabel;
	[SerializeField] private UIDropdownListCaliber _caliberSelection;
	[SerializeField] private UIWeaponSlot[] _weaponSlotsSelection;
	[SerializeField] private UIWeaponSlot[] _weaponSlots;
	[SerializeField] private bool _infiniteMoney;//DEBUG

	private WeaponData[] _weaponData;
	private int _cost;
	private int _initialCost;

	private void Update ()
	{
		UpdateCost();
		if (_fullAutoToggle.isOn)
		{
			_cost += (int)_priceForFullAuto;
		}
		_priceLabel.text = "" + _cost;
		_netWorthLabel.text = "" + _profileManager.GetProfileNetWorth();
	}

	private void UpdateCost ()
	{
		_cost	= (int)(_fireRateSlider.value * _pricePerFierRateUnit 
		              + _recoilDampingSlider.value * _pricePerRecoilDampingUnit
		              + _accuracySlider.value * _pricePerAccuracyUnit
		              + _reloadSpeedSlider.value * _pricePerReloadSpeedUnit
		              + _heatCoolingSlider.value * _pricePerHeatCoolingUnit
		              + _reliabilitySlider.value * _pricePerReliabilityUnit
		              + _ammoCapacityInventorySlider.value * _pricePerAmmoCapacityInventoryUnit
		              + _pricePerAmmoCapacityMagazineUnit * _pricePerAmmoCapacityMagazineUnit
		              - _initialCost);
	}

	public void OnProfileLoaded ()
	{
		//That's a workaround to subtract the initial cost of the default weapon and get the price of the upgraded weapon only.
		//Leads to prices bug when changing profiles. TODO: Instead create a set of variables for default weapon settings;
		Update();
		_initialCost = _cost;
		Update();
		BuyCurrentWeapon();//Set players weapon to current settings. 
		/////////////////////

		_weaponData = _profileManager.GetWeaponData();
		if (_weaponData == null)
		{
			_weaponData = new WeaponData[_weaponSlots.Length];
		}
		for (int i = 0; i < _weaponSlots.Length; i++)
		{
			_weaponSlots[i].SetIndex(i);
			_weaponSlotsSelection[i].SetIndex(i);
			if (_weaponData[i] != null)
			{
				_weaponSlots[i].SetData(_weaponData[i]._cost, _weaponData[i]._name);
				_weaponSlotsSelection[i].SetData(_weaponData[i]._cost, _weaponData[i]._name);
			}
			else
			{
				_weaponSlots[i].ClearData();
			}
		}
	}

	public void BuyCurrentWeapon ()
	{
		if (_profileManager.GetProfileNetWorth() >= _cost || _infiniteMoney)
		{
			_weapon.Set(_fireRateSlider.value, 
			            _recoilDampingSlider.value, 
			            _accuracySlider.value, 
			            _reloadSpeedSlider.value, 
			            _heatCoolingSlider.value, 
			            _reliabilitySlider.value, 
			            (int)_ammoCapacityMagazineSlider.value, 
			            (int)_ammoCapacityInventorySlider.value,
			            _fullAutoToggle.isOn,
			            _caliberSelection.GetCaliber());
			_weapon.Reset();
		}
	}

	public void SaveWeaponSlot (int slotIndex)
	{
		if (_profileManager.GetProfileNetWorth() >= _cost || _infiniteMoney)
		{
			WeaponData data = new WeaponData();
			data._fireRate = _fireRateSlider.value;
			data._damping = _recoilDampingSlider.value;
			//data._force = _caliberSelection.GetCaliber().GetForce();
			data._name = _nameInput.text;
			data._cost = _cost;
			data._accuracy = _accuracySlider.value;
			//data._damage = _caliberSelection.GetCaliber().GetPower();
			data._reloadSpeed = _reloadSpeedSlider.value;
			data._cooling = _heatCoolingSlider.value;
			data._reliability = _reliabilitySlider.value;
			data._ammoCapacityInventory = _ammoCapacityInventorySlider.value;
			data._ammoCapacityMagazine = _ammoCapacityMagazineSlider.value;
			data._fullAuto = _fullAutoToggle.isOn;

			data._caliber = _caliberSelection.GetCaliber().ToString();
			print(data._caliber);
			_weaponData[slotIndex] = data;
			_weaponSlots[slotIndex].SetData(data._cost, data._name);
			_weaponSlotsSelection[slotIndex].SetData(data._cost, data._name);
			_profileManager.SaveWeaponData(_weaponData);
			BuyCurrentWeapon();
		}
	}
	
	public void ClearWeaponSlot (int index)
	{
		_weaponData[index] = null;
		_profileManager.SaveWeaponData(_weaponData);
		_weaponSlots[index].ClearData();
		_weaponSlotsSelection[index].ClearData();
	}

	public void LoadWeapon (int slotIndex)
	{
		WeaponData data = _weaponData[slotIndex];
		if (data != null)
		{
			_nameInput.text = data._name;
			_fireRateSlider.value = data._fireRate;
			_recoilDampingSlider.value = data._damping;
			//_caliberSelection.GetCaliber().GetForce() = data._force;
			_accuracySlider.value = data._accuracy;
			//_projectilePowerSlider.value = data._damage;
			_reloadSpeedSlider.value = data._reloadSpeed;
			_cost = data._cost;
			_heatCoolingSlider.value = data._cooling;
			_reliabilitySlider.value = data._reliability;
			_ammoCapacityInventorySlider.value = data._ammoCapacityInventory;
			_ammoCapacityMagazineSlider.value = data._ammoCapacityMagazine;
			_fullAutoToggle.isOn = data._fullAuto;

			_caliberSelection.SetValueFromString(data._caliber);
			
		}
		BuyCurrentWeapon();
	}
}

[System.Serializable]
public class WeaponData
{
	public string _name;
	public int _cost;
	public float _fireRate;
	public float _damping;
	//public float _force;
	public float _accuracy;
	//public float _damage;
	public float _reloadSpeed;
	public float _reliability;
	public float _cooling;
	public float _ammoCapacityInventory;
	public float _ammoCapacityMagazine;
	public bool _fullAuto;

	public string _caliber;
}

