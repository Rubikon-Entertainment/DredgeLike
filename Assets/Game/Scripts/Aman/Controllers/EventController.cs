using System.Collections;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public static EventController instance { get; private set; }

    bool isFinished = false;
    int weight { get; set; }
    

    private void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    void StartGamePlay(Mode mode)
    {
        switch (mode)
        {
            case Mode.Rolling:
                Debug.Log("Game mode is Rolling");
                break;

            case Mode.Tapping:
                Debug.Log("Game mode is Tapping");
                break;

            case Mode.Wrestling:
                Debug.Log("Game mode is Wrestling");
                break;

            case Mode.ResourceCatching:
                Debug.Log("Game mode is Resource Catching");
                break;

            default:
                Debug.Log("Unknown game mode");
                break;
        }
    }
}

public enum Level  {Easy, Medium, Hard}
public enum Mode { Rolling, Tapping, Wrestling, ResourceCatching }