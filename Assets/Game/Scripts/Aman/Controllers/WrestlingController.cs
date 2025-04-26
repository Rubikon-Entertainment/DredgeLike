using UnityEngine;

public class WrestlingController : MonoBehaviour
{
    public static WrestlingController instance { get; private set; }
    public Fish fishPrefab;
    public Arrows arrowsPrefab;

    private void Awake()
    {
        Singleton();
        InitializeFish();
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

    private void InitializeFish()
    {
        Fish fish = Instantiate(fishPrefab);
        Arrows arrows = Instantiate(arrowsPrefab);
    }
}
