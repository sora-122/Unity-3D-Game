using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("3Dキャラクター (PlayerController) の参照")]
    public PlayerController playerController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // シーン遷移を考慮する場合は DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
