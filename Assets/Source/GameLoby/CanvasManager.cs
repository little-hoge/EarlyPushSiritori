using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    [SerializeField]
    private LobbyFunction lobbyFunction = default;
    public LobbyFunction LobbyFunction
    {
        get { return lobbyFunction; }
    }

    private void Awake()
    {
        Instance = this;
    }
}