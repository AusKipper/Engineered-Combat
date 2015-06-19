using UnityEngine;
using UnityEngine.UI;

public class UIDropdownListCaliber : UIDropdownList
{
	[SerializeField] private CaliberList _caliberList;

	protected override void InitiateValues ()
	{
		_values = (object[])_caliberList.GetCalibers();
	}

	public Caliber GetCaliber ()
	{
		return (Caliber)GetValue();
	}
}