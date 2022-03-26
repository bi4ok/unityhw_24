using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : MonoBehaviour
{
    private Animator _wizardAnimator;
    [SerializeField] private GameObject[] weapons;
    private Dictionary<string, string> _keyForTriggerPairs;
    private int _currentWeaponIndex = 0;


    void Start()
    {
        _wizardAnimator = GetComponent<Animator>();
        _keyForTriggerPairs = new Dictionary<string, string>()
        {
            ["Alpha1"] = "Idle",
            ["Alpha2"] = "Cast3",
            ["Alpha3"] = "Cast4",
            ["Alpha4"] = "Dance",
            ["Alpha5"] = "Die",
        };

    }

    void Update()
    {
        string triggerName = PressedKeyToTrigger();
        if (triggerName != "Wait")
        {
            _wizardAnimator.SetTrigger(triggerName);
        }
        if (Input.mouseScrollDelta.y != 0)
        {
            Debug.Log((-1)%3);
            int newWeaponIndex = (_currentWeaponIndex + (int) Input.mouseScrollDelta.y) % weapons.Length;
            newWeaponIndex = newWeaponIndex < 0 ? weapons.Length - 1 : newWeaponIndex;
            ChangeWeapon(_currentWeaponIndex, newWeaponIndex);
            _currentWeaponIndex = newWeaponIndex;
        }

    }

    private string PressedKeyToTrigger()
    {
        foreach (var keyName in _keyForTriggerPairs)
        {
            KeyCode thisKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyName.Key);
            if (Input.GetKeyDown(thisKeyCode))
            {
                return keyName.Value;
            }
        }
        return "Wait";
    }

    private void ChangeWeapon(int from, int to)
    {
        weapons[from].SetActive(false);
        weapons[to].SetActive(true);
    }
}
