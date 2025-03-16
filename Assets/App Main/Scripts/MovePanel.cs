// MovePanel.cs
// スマホ下部のタッチ領域の動作を処理するクラス
using UnityEngine;
using UnityEngine.EventSystems;

public class MovePanel : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private IMoveCommand moveCommand;
    private bool isDragging = false;

    private void Awake()
    {
        // ContinuousMoveCommand のインスタンスを生成
        moveCommand = new ContinuousMoveCommand();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        ProcessTouch(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            ProcessTouch(eventData);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        Debug.Log("MovePanel: Touch released, stopping move input.");
    }

    // タッチ位置を処理し、操作領域内の場合のみコマンドを実行する
    private void ProcessTouch(PointerEventData eventData)
    {
        RectTransform rectTransform = transform as RectTransform;
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            // 操作領域の内部のみ処理する
            if (rectTransform.rect.Contains(localPoint))
            {
                // パネル中心からの正規化された方向を算出
                Vector2 normalizedDirection = localPoint.normalized;
                moveCommand.Execute(normalizedDirection);
            }
            else
            {
                Debug.Log("MovePanel: Touch is outside of the valid area.");
            }
        }
    }
}
