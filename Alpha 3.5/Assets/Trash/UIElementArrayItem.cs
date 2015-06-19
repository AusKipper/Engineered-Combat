//using UnityEngine;
//using UnityEngine.UI;
//
//public class UILevelPage : MonoBehaviour 
//{
//	[SerializeField] private RectTransform _transform;
//	[SerializeField] private GameObject _buttonPrefab;
//	[SerializeField] private GameObject _pagePrefab;
//	[SerializeField] private int _buttonsPerPage;
//	[SerializeField] private int _buttonSpacing;
//
//	private GameObject[] _pages;
//	private int _currentPageIndex;
//	private	int _numbeOfPages;
//
//	public void Start ()
//	{
//		int numberOfButtons = Application.levelCount - 2;
//		_numbeOfPages = (int)(numberOfButtons / _buttonsPerPage) + 1;
//		_pages = new GameObject[_numbeOfPages];
//		for (int p = 0, b = 0; p < _numbeOfPages; p++)
//		{
//			GameObject page = Instantiate(_pagePrefab) as GameObject;
//			page.transform.SetParent(_transform, false);
//			_pages[p] = page;
//			if (p != 0)
//			{
//				page.SetActive(false);
//			}
//			for (int i = 0; i < _buttonsPerPage && b < numberOfButtons; i++, b++)
//			{
//				UILevelButton button = (Instantiate(_buttonPrefab) as GameObject).GetComponent<UILevelButton>();
//				button.transform.SetParent(page.transform, false);
//				button.SetIndex(b);
//				//button.OnEnable();
//				button.transform.Translate(new Vector3(0f, -i * _buttonSpacing, 0f));
//				button.gameObject.SetActive(true);
//			}
//		}
//	}
//
//	public void GoToNextPage ()
//	{
//		if (_currentPageIndex < _numbeOfPages - 1)
//		{
//			_pages[_currentPageIndex].SetActive(false);
//			_pages[++_currentPageIndex].SetActive(true);
//		}
//	}
//
//	public void GoToPreviousPage ()
//	{
//		if (_currentPageIndex > 0)
//		{
//			_pages[_currentPageIndex].SetActive(false);
//			_pages[--_currentPageIndex].SetActive(true);
//		}
//	}
//}
