using System.Collections;
using UnityEngine;
using Single;

public class Player : Singleton<Player>
{

    public PlayerMove Move { get; private set; }
    public PlayerSelecting Select { get; private set; }
    public PlayerActions Action { get; private set; }
    public PlayerTalk Talk { get; private set; }
    public PlayerCamera Camera { get; private set; }


    //Управление

    //Действия
    private bool think;



    private void Awake()
    {
        Move = GetComponent<PlayerMove>();
        Move.SetupMove(GetComponent<CharacterController>());

        Select = new PlayerSelecting();

        Action = new PlayerActions();

        Talk = GetComponent<PlayerTalk>();

        Camera = PlayerCamera.get;
    }


    private void Update()
    {
        Select.Update();
        Action.Update();
    }
}
