using UnityEngine;

public class EventController : MonoBehaviour
{
    public static EventController instance { get; private set; }
    [SerializeField] private QTEController controller;
    public Level currentLevel;


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

    private void Start()
    {
        StartGamePlay();
    }

    void StartGamePlay()
    {
        //TODO Передать параметры рыбы
           controller.StartGame(Level.Medium);
    }
}

public enum Level  {Easy = 8, Medium = 11, Hard = 15}