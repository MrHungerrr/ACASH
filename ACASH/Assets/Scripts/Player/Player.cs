using System.Collections;
using UnityEngine;
using Single;

public class Player : Singleton<Player>
{

    public PlayerMove Move { get; protected set; }
    public PlayerSelecting Select { get; protected set; }
    public PlayerActions Action { get; protected set; }
    public PlayerTalkControl Talk { get; protected set; }
    public PlayerCamera Camera { get; protected set; }



    protected void Awake()
    {
        Setup();
    }


    protected virtual void Setup()
    {
        Move = GetComponent<PlayerMove>();
        Move.Setup();

        Camera = GetComponentInChildren<PlayerCamera>();
        Camera.Setup(this);

        Select = new PlayerSelecting();

        Action = new PlayerActions();

        Talk = GetComponent<PlayerTalkControl>();
    }


    protected void Update()
    {
        Select.Update();
        Action.Update();
    }
}
