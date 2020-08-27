using System.Collections;
using UnityEngine;
using Single;

public class Player : MonoSingleton<Player>
{


    public PlayerSelecting Select { get; protected set; }
    public PlayerActions Action { get; protected set; }
    public PlayerHear Hear { get; protected set; }
    public PlayerCamera Camera => _camera;
    public PlayerMove Move => _move;
  

    [SerializeField] private PlayerMove _move;
    [SerializeField] private PlayerCamera _camera;



    private void Awake()
    {
        Setup();
    }


    protected virtual void Setup()
    {
        Move.Setup();
        Camera.Setup(this);

        Select = new PlayerSelecting();
        Action = new PlayerActions();
        Hear = new PlayerHear();
    }


    private void Update()
    {
        if (InputManager.GameType == InputManager.GameplayType.FirstPerson)
        {
            Select.Update();
        }
    }
}
