using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private FinishPoint[] _finishPoints;
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        _player.Dying += DeadText;
        foreach (var finishPoint in _finishPoints)
        {
            finishPoint.GameFinished += EscapeText;
        }
    }

    private void Unsubscrible()
    {
        _player.Dying -= DeadText;
        foreach (var finishPoint in _finishPoints)
        {
            finishPoint.GameFinished -= EscapeText;
        }
    }

    private void DeadText()
    {
        _text.color = Color.red;
        _text.text = "Вы мертвы";
        this.gameObject.SetActive(true);
        Cursor.visible = true;
        Unsubscrible();
    }

    private void EscapeText()
    {
        _text.color = Color.green;
        _text.text = "Вы спаслись";
        this.gameObject.SetActive(true);
        Cursor.visible = true;
        Unsubscrible();
    }
}
