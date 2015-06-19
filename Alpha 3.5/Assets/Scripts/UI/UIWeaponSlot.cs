using UnityEngine;
using UnityEngine.UI;

public class UIWeaponSlot : MonoBehaviour 
{
	[SerializeField] private WeaponEditor _weaponEditor;
	[SerializeField] private Text _costText;
	[SerializeField] private Text _nameText;

	private int _index;

	public void SetIndex (int index)
	{
		_index = index;
	}

	public void SetData (int cost, string name)
	{
		_costText.text = "" + cost;
		_nameText.text = "" + name;
	}

	public void ClearData ()
	{
		_costText.text = "...";
		_nameText.text = "...";
	}

	public void OnLoadButton ()
	{
		_weaponEditor.LoadWeapon(_index);
	}

	public void OnDeleteButton ()
	{
		_weaponEditor.ClearWeaponSlot(_index);
	}

	public void OnSaveButton ()
	{
		_weaponEditor.SaveWeaponSlot(_index);
	}
}