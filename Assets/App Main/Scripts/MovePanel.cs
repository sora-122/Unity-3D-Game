// MovePanel.cs
// スマホ下部のタッチ領域の動作を処理するクラス
using UnityEngine;
using UnityEngine.EventSystems;

public class MovePanel : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private IMoveCommand moveCommand;
    private bool isDragging = false;
    private Vector2 _lastDirection = Vector2.zero;
    private RectTransform _rectTransform;

    private void Awake()
    {
        // ContinuousMoveCommand のインスタンスを生成
        moveCommand = new ContinuousMoveCommand();
        _rectTransform = transform as RectTransform;
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
        _lastDirection = Vector2.zero;
        moveCommand.Execute(_lastDirection);
        Debug.Log("MovePanel: Touch released, stopping move input.");
    }

    // タッチ位置を処理し、操作領域内の場合のみコマンドを実行する
    private void ProcessTouch(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            // 操作領域の内部のみ処理する
            if (_rectTransform.rect.Contains(localPoint))
            {
                // パネル中心からの方向を算出（正規化はしない）
                Vector2 direction = (localPoint - _rectTransform.rect.center);
                // パネルのサイズを考慮して正規化
                Vector2 normalizedDirection = new Vector2(direction.x / (_rectTransform.rect.width / 2), direction.y / (_rectTransform.rect.height / 2));
                // 正規化された方向を-1〜1の範囲にクランプ
                normalizedDirection = Vector2.ClampMagnitude(normalizedDirection, 1f);
                _lastDirection = normalizedDirection;
                moveCommand.Execute(_lastDirection);
            }
            else
            {
                Debug.Log("MovePanel: Touch is outside of the valid area.");
            }
        }
    }
}
