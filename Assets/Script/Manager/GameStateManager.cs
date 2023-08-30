using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private static GameStateManager instance;

    public static GameStateManager getInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<GameStateManager>();
        }
        return instance;
    }

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public Publisher publisherGameState = new Publisher();
    Game_State state = Game_State.Play;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(state == Game_State.Play)
            {
                setState(Game_State.Pause);
            }
            else setState(Game_State.Play);
        }
    }

    public void setState(Game_State state)
    {
        if(this.state != state)
        {
            switch(state)
            {
                case Game_State.Play:
                    Debug.Log("Play");
                    break;
                case Game_State.Pause:
                    Debug.Log("Pause");
                    break;
            }

            publisherGameState.notify((int)state);
            this.state = state;
        }
    }
}

public class Publisher
{
    List<Subcriber> subcribers = new List<Subcriber> ();

    public void subcribe(Subcriber subcriber)
    {
        subcribers.Add(subcriber);
    }

    public void unsubcribe(Subcriber subcriber)
    {
        subcribers.Remove(subcriber);
    }

    public void notify(int state)
    {
        foreach (var subcriber in subcribers)
        {
            subcriber.update(state);
        }
    }
}

public interface Subcriber
{
    public abstract void update(int state);
}

public enum Game_State
{
    Pause = 1,
    Play = 2,
}
