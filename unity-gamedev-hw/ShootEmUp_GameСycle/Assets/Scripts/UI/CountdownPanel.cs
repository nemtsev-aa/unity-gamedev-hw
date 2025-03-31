using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CountdownPanel : MonoBehaviour {
    public event Action CountdownFinished;

    [SerializeField] int duration = 3;
    [SerializeField] private TextMeshProUGUI _timeValueText;

    public void Show(bool value) {
        gameObject.SetActive(value);

        if (value)
            StartCoroutine(Countdown());
    }

    private IEnumerator Countdown() {
        for (int i = duration; i >= 0; i--) {
            _timeValueText.text = i.ToString();

            Sequence countdownSequence = DOTween.Sequence().SetEase(Ease.Linear);
            countdownSequence.Append(_timeValueText.transform.DOScale(Vector3.one, 0.5f));
            countdownSequence.Append(_timeValueText.transform.DOScale(Vector3.zero, 0.5f));

            countdownSequence.Play();
            yield return countdownSequence.WaitForCompletion();
        }

        CountdownFinished?.Invoke();
    }
}
