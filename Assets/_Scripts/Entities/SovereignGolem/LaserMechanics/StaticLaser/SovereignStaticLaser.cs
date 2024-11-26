﻿using System.Collections;
using UnityEngine;

public class SovereignStaticLaser : SovereignLaser {

    public event System.Action<SovereignStaticLaser> OnLaserEnd;

    [Header("Static Laser Values")]
    [SerializeField] private SovereignLaserWarning warning;
    [SerializeField] private GameObject vfxPrefab;

    public void Activate(float warningTime, float duration) {
        warning.DoWarning(warningTime);
        warning.OnWarningFinished += () => {
            DoLaserAttack(duration);
        };
    }

    private void DoLaserAttack(float duration) {
        StopAllCoroutines();
        StartCoroutine(IDoLaserAttack(duration));
    }

    private IEnumerator IDoLaserAttack(float duration) {
        vfxPrefab.SetActive(true);

        float timer = 0;
        while (timer < duration) {
            timer += Time.deltaTime;
            yield return null;
        }
        OnLaserEnd?.Invoke(this);

        vfxPrefab.SetActive(false);
    }
}