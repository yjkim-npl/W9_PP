using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventHandler : MonoBehaviour,
    IPointerClickHandler, IBeginDragHandler,
    IDragHandler, IEndDragHandler, IPointerDownHandler,
    IPointerUpHandler
{
    // Click, P_Down, P_Up, B_Drag, Drag, E_Drag
    public Action<PointerEventData>[] actions = new Action<PointerEventData>[Enum.GetNames(typeof(Define.UIEvent)).Count()];
    public void OnEventAction(PointerEventData eventData, Define.UIEvent type = Define.UIEvent.Click)
    {
        actions[(int)type]?.Invoke(eventData);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        actions[(int)Define.UIEvent.Click].Invoke(eventData);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        actions[(int)Define.UIEvent.BeginDrag].Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        actions[(int)Define.UIEvent.Drag].Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        actions[(int)Define.UIEvent.EndDrag].Invoke(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        actions[(int)Define.UIEvent.PointerDown].Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        actions[(int)Define.UIEvent.PointerUp].Invoke(eventData);
    }

}