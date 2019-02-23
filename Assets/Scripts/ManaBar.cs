using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private Slider _manaBar;
    // Start is called before the first frame update
    void Start()
    {
        _player.UpdateMana += UpdateManaBar;
    }

    // Update is called once per frame
    private void UpdateManaBar()
    {
        _manaBar.value = _player.mana;
    }
}
