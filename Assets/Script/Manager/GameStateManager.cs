using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Publisher publisherGameDontDestroyState = new Publisher();
    Game_State state = Game_State.Play;

    private void Start()
    {
        addSubcriberDontDestroy();
    }

    private void Update()
    {

    }

    public void addSubcriberDontDestroy()
    {
        publisherGameDontDestroyState.subcribe(GameObject.FindObjectOfType<Player>());
        publisherGameDontDestroyState.subcribe(PlayerControl.getInstance());
        publisherGameDontDestroyState.subcribe(SoundManager.getInstance());
        publisherGameDontDestroyState.subcribe(SceneLoader.getInstance());
        publisherGameDontDestroyState.subcribe(UIManager.getInstance());
    }

    public void setState(Game_State state)
    {
        if(this.state != state)
        {
            switch (state)
            {
                case Game_State.Play:
                    Debug.Log("Play");
                    publisherGameState.notify((int)state);
                    break;
                case Game_State.Pause:
                    Debug.Log("Pause");
                    publisherGameState.notify((int)state);
                    break;
                case Game_State.BacktoMenu:
                    publisherGameDontDestroyState.notify((int)state);
                    SceneManager.LoadScene(0);
                    break;
            }
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
    BacktoMenu = 3,
}
