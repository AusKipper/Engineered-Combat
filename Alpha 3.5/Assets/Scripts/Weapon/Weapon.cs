using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour 
{
	[SerializeField] private Transform _transform;
	[SerializeField] private Transform _CameraTransform;
	[SerializeField] private Transform _launcher;
	[SerializeField] private Transform _hipWeaponAnchor;
	[SerializeField] private Transform _centerWeaponAnchor;
	[SerializeField] private UICrosshair _crosshair;
	[SerializeField] private UIHud _hud;
	[SerializeField] private Player _player;
	[SerializeField] private Magazine _magazine;//Limited use.
	[SerializeField] private float _accuracySpreadMax;
	[SerializeField] private float _recoilSpread;
	[SerializeField] private float _recoilUp;
	[SerializeField] private float _recoilSpreadPerStack;
	[SerializeField] private float _recoilBackDelay;
	[SerializeField] private float _heatCapacity;
	[SerializeField] private float _heatPerShot;
	[SerializeField] private float _reloadTime;
	[SerializeField] private float _jamTime;
	[SerializeField] private GameObject _projectilePrefab;
	[SerializeField] private Projectile _projectile;

	private string _name;
	private float _accuracy;
	private float _fireRate;
	private float _recoilDamping;
	private float _reliability;
	private float _reloadSpeed;
	private float _coolingRate;
	private float _cost;
	private int _ammoCapacityWeapon;
	private int _ammoCapacityInventory;
	private ActionType _actionType;
	private Quality _buildQuality;

	private float _heat;
	private bool _aiming;
	private bool _overheated;
	private bool _jammed;
	private bool _reloading;
	private bool _onCooldown;
	private bool _triggerPulled;
	private int _recoilStacks;
	private int _ammoCountWeapon;
	private int _ammoCountInventory;
	
	private void Update ()
	{
		if (_heat > 0)
		{
			_heat -= _coolingRate * Time.deltaTime;
			if (_heat < 0)
			{
				_heat = 0;
			}
		}
	}

	public void PullTrigger ()
	{
		_triggerPulled = true;
		Fire();
	}

	public void ReleaseTrigger ()
	{
		_triggerPulled = false;
	}

	public void Reload ()
	{
		if (_ammoCountWeapon != _ammoCapacityWeapon && _ammoCountInventory != 0 && !_jammed && !_overheated)
		{
			StartCoroutine("ReloadCoroutine");
		}
	}

	public void ToggleAim ()
	{
		if (!_reloading && !_triggerPulled)
		{
			if (!_aiming)
			{
				_aiming = true;
				_transform.position = _centerWeaponAnchor.position;
				//_transform.rotation = _centerWeaponAnchor.rotation;
				_crosshair.Disable();
			}
			else
			{
				_aiming = false;
				_transform.position = _hipWeaponAnchor.position;
				//_transform.rotation = _hipWeaponAnchor.rotation;
				_crosshair.Enable();
			}
		}
	}

	public void Set (float fireRate, float recoilDamping, float accuray, float reloadSpeed, float heatCooling, float reliability, int ammoCapacityWeapon, int ammoCapacityInventory, bool fullAuto, Caliber caliber)
	{
		_fireRate = fireRate;
		_recoilDamping = recoilDamping;
		_accuracy = accuray;
		_reloadSpeed = reloadSpeed;
		_coolingRate = heatCooling;
		_reliability = reliability;
		_ammoCapacityWeapon = ammoCapacityWeapon;
		_ammoCapacityInventory = ammoCapacityInventory;		
		_ammoCountWeapon = ammoCapacityWeapon;
		_ammoCountInventory = ammoCapacityInventory;
		if (fullAuto)
		{
			_actionType = ActionType.FullAuto;
		}
		else
		{
			_actionType = ActionType.SemiAuto;
		}
		_projectile.SetCaliber(caliber);
	}

	private void Fire ()
	{
		if (_onCooldown == false && _ammoCountWeapon != 0 && !_reloading && !_jammed && !_overheated)
		{
			_ammoCountWeapon--;
			float accuracySpread = _accuracySpreadMax * (1 - _accuracy);
			float accuracySpreadX = Random.Range(-accuracySpread, accuracySpread);
			float accuracySpreadY = Random.Range(-accuracySpread, accuracySpread);
			Vector3 launcherRotation = _launcher.rotation.eulerAngles;
			//_transform.Rotate(accuracySpreadX, accuracySpreadY, 0f, Space.Self);
			Quaternion rotation =  Quaternion.Euler(new Vector3(accuracySpreadX + launcherRotation.x, accuracySpreadY + launcherRotation.y, 0f + launcherRotation.z));
			Projectile projectile = (Instantiate(_projectilePrefab, _launcher.position, rotation) as GameObject).GetComponent<Projectile>();
			projectile.Fire();
			StartCoroutine("RecoilCoroutine");
			StartCoroutine("CooldownCoroutine");
			_heat += _heatPerShot;
			if (_heat >= _heatCapacity)
			{
				StartCoroutine("OverheatCoroutine");
			}
			else if (Random.Range(0f,1f) > _reliability)
			{
				StartCoroutine("JamCoroutine");
			}
		}
	}

	private IEnumerator CooldownCoroutine ()
	{
		_onCooldown = true;
		yield return new WaitForSeconds(60 / _fireRate);
		_onCooldown = false;
		if (_triggerPulled)
		{
			if (_actionType == ActionType.FullAuto || _actionType == ActionType.BoltRepeater)
			{
				Fire();
			}
		}
	}

	private IEnumerator RecoilCoroutine ()
	{
		float recoilRange = _recoilSpreadPerStack * _recoilStacks + _recoilSpread;
		Vector2 recoil = new Vector2((Random.Range(-recoilRange, recoilRange) + _recoilUp) * (1 - _recoilDamping), (Random.Range(-recoilRange, recoilRange)) * (1 - _recoilDamping));
		_recoilStacks++;
		_crosshair.SetSpread(_recoilStacks);
		_transform.Rotate(recoil.x, recoil.y, 0f, Space.Self);
		yield return new WaitForSeconds(_recoilBackDelay);
		_transform.Rotate(-recoil.x, -recoil.y, 0f, Space.Self);
		_recoilStacks--;
		_crosshair.SetSpread(_recoilStacks);
	}

	private IEnumerator ReloadCoroutine ()
	{
		_reloading = true;
		int roundsToReload = Mathf.Min(_ammoCapacityWeapon - _ammoCountWeapon, _ammoCountInventory);
		_transform.Rotate(new Vector3(-40f, 0f, 0f), Space.Self);
		yield return new WaitForSeconds(Mathf.Max(_reloadTime / _reloadSpeed, 60 / _fireRate));
		_transform.Rotate(new Vector3(40f, 0f, 0f), Space.Self);
		_ammoCountWeapon += roundsToReload;
		_ammoCountInventory -= roundsToReload;
		_reloading = false;
		ResetRotation();
	}

	private IEnumerator JamCoroutine ()
	{
		_jammed = true;
		_hud.OnWeaponJam(_jamTime);
		_transform.Rotate(new Vector3(0f, -70f, 0f), Space.Self);
		yield return new WaitForSeconds(_jamTime);
		_transform.Rotate(new Vector3(0f, 70f, 0f), Space.Self);
		_jammed = false;
		ResetRotation();
	}

	private IEnumerator OverheatCoroutine () 
	{
		_overheated = true;
		float duration = _heatCapacity * 0.5f / _coolingRate;
		_hud.OnWeaponOverheat(duration);
		_transform.Rotate(new Vector3(-20f, 0f, 0f), Space.Self);
		yield return new WaitForSeconds(duration);
		_transform.Rotate(new Vector3(20f, 0f, 0f), Space.Self);
		_overheated = false;
	}

	private void ResetRotation ()
	{
		StopCoroutine("RecoilCoroutine");
		_transform.localRotation = Quaternion.identity;
	}

	public void Reset ()
	{
		StopAllCoroutines();
		ReleaseTrigger();
		_hud.Reset();
		_heat = 0;
		_onCooldown = false;
		_overheated = false;
		_reloading = false;
		_jammed = false;
		_aiming = false;
		_ammoCountWeapon = _ammoCapacityWeapon;
		_ammoCountInventory = _ammoCapacityInventory;

		_transform.position = _hipWeaponAnchor.position;//TODO: Make it into method;
		_transform.rotation = _hipWeaponAnchor.rotation;//
		_crosshair.Enable();							//
	}

	public int GetAmmoCountWeapon ()
	{
		return _ammoCountWeapon;
	}

	public int GetAmmoCountInventory ()
	{
		return _ammoCountInventory;
	}

	public float GetHeatValue ()
	{
		return _heat;
	}

	public float GetHeatCapacity ()
	{
		return _heatCapacity;
	}
}