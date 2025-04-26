using UnityEngine;
using UnityEngine.UI;

public class QTEController : MonoBehaviour {

    [SerializeField] private Slider slider;
    [SerializeField] private RectTransform[] successLines;

    private float sliderNum = 0f;
    private float[] randomNums = { 0, 0, 0 };
    [SerializeField] private float errorMargin = 3f;
    [SerializeField] private float sliderSpeed = 100f;
    private int progress = 5;
    [SerializeField] private int requiredProgress = 10;
    private bool isPlaying = false, isSuccess = false, isHundred = false;

    [SerializeField] private Transform fish;

    void Update()
    {
        if (!isPlaying) return;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Tap();
        }
        CalculateSliderNum();
    }

    public void StartGame(Level diff)
    {
        randomizeNumbers();
        sliderNum = 0f;
        slider.value = 0f;
        progress = 5;
        isPlaying = true;
        isSuccess = false;
        requiredProgress = (int)diff;
    }

    private void EndGame()
    {
        isPlaying = false;
        if (isSuccess)
        {
            //TODO: Передать рыбу через InventoryController
            Debug.Log($"Victory");
        }
    }

    private void Tap()
    {
        for (int i = 0; i < randomNums.Length; i++)
        {
            if (sliderNum >= randomNums[i] - errorMargin && sliderNum <= randomNums[i] + errorMargin)
            {
                FishSwimCloser();
                randomizeNumber(i);
                return;
            }
        }
        FishSwimAway();
    }

    private void CalculateSliderNum()
    {
        if (!isHundred)
        {
            sliderNum += sliderSpeed * Time.deltaTime;
        }
        else
        {
            sliderNum -= sliderSpeed * Time.deltaTime;
        }
        if (sliderNum >= 100f)
        {
            isHundred = true;
        } else if (sliderNum <= 0f)
        {
            isHundred = false;
        }
        slider.value = sliderNum;
    }

    private void FishSwimAway()
    {
        progress--;
        UpdateZPosition(0.1f);
        if (progress <= 0)
        {
            EndGame();
        }
    }

    private void FishSwimCloser()
    {
        progress++;
        UpdateZPosition(-0.1f);
        if (progress >= requiredProgress)
        {
            isSuccess = true;
            EndGame();
        }
    }

    private void randomizeNumbers() {
        for (int i = 0; i < randomNums.Length; i++) {
            randomNums[i] = Random.Range(3f, 97f);
        }
        float sliderWidthOne = slider.GetComponent<RectTransform>().sizeDelta.x / 100f;
        for (int i = 0; i < randomNums.Length; i++)
        {
            successLines[i].offsetMin = new Vector2(sliderWidthOne * (randomNums[i] - errorMargin), successLines[i].offsetMin.y);
            successLines[i].offsetMax = new Vector2(-slider.GetComponent<RectTransform>().sizeDelta.x + sliderWidthOne * (randomNums[i] + errorMargin), successLines[i].offsetMax.y);
        }
    }

    private void randomizeNumber(int _num)
    {
        randomNums[_num] = Random.Range(3f, 97f);
        float sliderWidthOne = slider.GetComponent<RectTransform>().sizeDelta.x / 100f;
        successLines[_num].offsetMin = new Vector2(sliderWidthOne * (randomNums[_num] - errorMargin), successLines[_num].offsetMin.y);
        successLines[_num].offsetMax = new Vector2(-slider.GetComponent<RectTransform>().sizeDelta.x + sliderWidthOne * (randomNums[_num] + errorMargin), successLines[_num].offsetMax.y);
    }

    public void UpdateZPosition(float num)
    {
        fish.transform.position = new Vector3(fish.transform.position.x, fish.transform.position.y, fish.transform.position.z + num);
    }
}
public enum Difficulty { hard = 15, medium = 11, easy = 8 };
