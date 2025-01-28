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
    [SerializeField] Text cargoText;

    private int maxCargoAmount;
    public int currentCargoAmount;
    private string maxCargoText;

    private float maxSpeed;
    private float minSpeed;
    private float currentSpeed;

    [SerializeField] float maxTimeSeconds = 100f;
    private float timeLeft = 0f;

    public Transform PauseMenu;


    void Start()
    {
        player = GameObject.FindWithTag("Boat").transform;
        shipMovement = player.GetComponent<ShipMovement>();
        

        maxSpeed = shipMovement.maxForwardSpeed;
        minSpeed = shipMovement.maxBackwardSpeed;
        currentSpeed = 0f;

        speedSlider.maxValue = maxSpeed;
        speedSlider.minValue = minSpeed;

        timeLeft = maxTimeSeconds;

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

        timeLeft -= Time.deltaTime;
        timer.text = timeLeft.ToString("0.00") + " s";

        cargoSlider.value = currentCargoAmount;
        cargoText.text = currentCargoAmount.ToString("0");

        if(currentCargoAmount <= 0 || timeLeft <= 0f)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        PauseMenu.gameObject.SetActive(true);
        PauseMenu.GetChild(PauseMenu.childCount-1).GetComponent<Text>().text = "Game Over";
        //Time.timeScale = 0.0f;
        Cursor.visible = true;
    }

}
