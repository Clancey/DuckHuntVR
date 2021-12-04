using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class VrGazeClicker : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
	public float activationTime = 3.0f;
	
	public UnityEvent onAction;

	private float timer = -1;

	public void OnPointerEnter(PointerEventData eventData)
	{
		timer = 0;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		timer = -1;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		ExecuteAction();
	}

	void Update()
	{
		if (timer < 0)
			return;

		timer += Time.deltaTime;
		if (timer >= activationTime)
		{
			ExecuteAction();
		}
	}

	private void ExecuteAction()
	{
		onAction.Invoke();
		timer = -1;
	}
}
