using Assets.Game.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SummaryController : MonoBehaviour
{
    public GameObject _startNextWaveObject;
    private Button _startNextWaveButton;

    private GameObject _gameControllerObject;
    private GameController _gameController;

    private ArrayList _avaliableWeapons = new ArrayList();

    private string _primaryWeapon;
    private string _secondaryWeapon;

    public Text baseRepairsText;
    public Text suppliesFoundSummary;

    private void Start()
    {
        _startNextWaveObject = GameObject.FindWithTag("StartNextWave");
        _gameControllerObject = GameObject.FindWithTag("GameController");

        _startNextWaveButton = _startNextWaveObject.GetComponent<Button>();
        _startNextWaveButton.onClick.AddListener(ConfirmButtonClicked);

        if (_gameControllerObject != null)
        {
             _gameController = _gameControllerObject.GetComponent<GameController>();
        }

    }

    private void ConfirmButtonClicked()
    {
        _gameController.updateSelectedWeapons(_primaryWeapon,_secondaryWeapon);
        _gameController.StartNextWave();
    }

    public void addWeaponToAvaliableWeapons(string weapon)
    {
        Debug.Log("Weapon added " + weapon);
        _avaliableWeapons.Add(weapon);
        _gameController.updateAllAvaliableWeapons(_avaliableWeapons);
    }

    public void updateBaseRepairsText(string newText)
    {
        baseRepairsText.text = newText;
    }

    public void updateSuppliesFoundSummary(string newText)
    {
        suppliesFoundSummary.text = newText;
    }

}

   