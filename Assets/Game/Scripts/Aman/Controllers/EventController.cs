using UnityEngine;

public class EventController : MonoBehaviour
{
    public static EventController instance { get; private set; }
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
        StartGamePlay(true);
        ProgressController.instance.SetDeltaValue(currentLevel);
    }

    void StartGamePlay(bool isFishing)
    {
        if (isFishing)
        {
            //TappingController.instance.StartGame();
            WrestlingController.instance.StartGame();

            //int randomGame = Random.Range(0, 3);
            //switch (randomGame)
            //{
            //    case 0:
            //        Debug.Log("Starting rolling game...");
            //        rollingController = RollingController.instance;
            //        rollingController.enabled = true;
            //        break;
            //    case 1:
            //        Debug.Log("Starting tapping game...");
            //        tappingController = TappingController.instance;
            //        tappingController.enabled = true;
            //        break;
            //    case 2:
            //        Debug.Log("Starting wrestling game...");
            //        wrestlingController = WrestlingController.instance;
            //        wrestlingController.enabled = true;
            //        break;
            //    default:
            //        Debug.LogError("Unknown game type selected!");
            //        break;
            //}
        }
        else
        {
            Debug.Log("Starting recource catching game...");
            ResourceCatchingController.instance.StartGame();
        }
    }
}

public enum Level  {Easy, Medium, Hard}