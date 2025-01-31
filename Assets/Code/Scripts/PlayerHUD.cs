using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    private ShipMovement shipMovement;
    private Transform player;

    private GridData gridData;
    [SerializeField] Slider cargoSlider;
    [SerializeField] Slider speedSlider;
    [SerializeField] Text timer;
    [SerializeField] Image speedHandle;
    [SerializeField] Slider timerSlider;
    [SerializeField] Text cargoText;

    public int maxCargoAmount;
    public int currentCargoAmount;
    private string maxCargoText;

    private float maxSpeed;
    private float minSpeed;
    private float currentSpeed;

    [SerializeField] float maxTimeSeconds = 100f;
    private float timeLeft = 0f;

    public Transform PauseMenu;

    public float valueDistance;
    public float sliderAddValue;

    public int finalCargo;



    void OnEnable()
    {
      

    }

    public void InitHUD()
    {
        player = GameObject.FindWithTag("Boat").transform;
        shipMovement = player.GetComponent<ShipMovement>();


        maxSpeed = shipMovement.maxForwardSpeed;
        minSpeed = shipMovement.maxBackwardSpeed;
        currentSpeed = 0f;

        speedSlider.maxValue = maxSpeed;
        speedSlider.minValue = minSpeed;

        timeLeft = maxTimeSeconds;

        timerSlider.maxValue = maxTimeSeconds;
        timerSlider.minValue = 0;

        gridData = GameObject.FindWithTag("CargoData").transform.GetComponent<GridData>();

        maxCargoAmount = gridData.placedObjects.Count;
        //maxCargoAmount = 20;
        currentCargoAmount = maxCargoAmount;
        maxCargoText = maxCargoAmount.ToString("0");

        cargoSlider.maxValue = maxCargoAmount;
        cargoSlider.minValue = 0f;
    }

    void Update()
    {
        currentSpeed = shipMovement.currentDesiredSpeed;
        speedSlider.value = currentSpeed;

        float speedHandleRotation;
        if (currentSpeed > 0)
        {
            float positiveSpeed = Mathf.InverseLerp(0, maxSpeed, currentSpeed);
            speedHandleRotation = Mathf.Lerp(0f, -105f, positiveSpeed);
        }
        else
        {
            float negativeSpeed = Mathf.InverseLerp(0, minSpeed, currentSpeed);
            speedHandleRotation = Mathf.Lerp(0f, 105f, negativeSpeed);
        }

        speedHandle.rectTransform.rotation = Quaternion.Euler(0, 0, speedHandleRotation);

        timeLeft -= Time.deltaTime;
        timer.text = toMinutes();

        cargoSlider.value = currentCargoAmount;
        cargoText.text = currentCargoAmount.ToString("0");

        timerSlider.value = timeLeft;

        if (currentCargoAmount <= 0 || timeLeft <= 0f)
        {
            GameOver();
        }
    }

    private string toMinutes()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60f);
        int seconds = Mathf.FloorToInt(timeLeft - minutes * 60);

        string newTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        return newTime;
    }

    void GameOver()
    {
        PauseMenu.gameObject.SetActive(true);
        PauseMenu.GetChild(PauseMenu.childCount-1).GetComponent<Text>().text = "Game Over";
        PauseMenu.GetChild(2).gameObject.SetActive(false);
        //Time.timeScale = 0.0f;
        Cursor.visible = true;
    }

    public void InGoal()
    {
        finalCargo = currentCargoAmount;
    }
}
