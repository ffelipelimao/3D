using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public List<GunUI> uiGun;

    public float maxShoot = 5f;
    public float timeToReload = 1f;
    private float _currentShoots;
    private bool _reloading;

    void Awake()
    {
        GetAllUI();
    }

    protected override IEnumerator ShootCoroutine()
    {
        if (_reloading) yield break;
        while (true)
        {
            if (_currentShoots < maxShoot)
            {
                Shoot();
                _currentShoots++;
                CheckReload();
                UpdateUI();
                yield return new WaitForSeconds(timeBetweenShoot);
            }

        }
    }
    private void CheckReload()
    {
        if (_currentShoots >= maxShoot)
        {
            StopShoot();
            StartReload();
        }
    }

    void StartReload()
    {
        _reloading = true;
        StartCoroutine(ReloadCoroutine());

    }

    IEnumerator ReloadCoroutine()
    {
        float time = 0;
        while (time < timeToReload)
        {
            time += Time.deltaTime;
            uiGun.ForEach(i => i.UpdateValue(time / timeToReload));
            yield return new WaitForEndOfFrame();
        }
        _currentShoots = 0;
        _reloading = false;
    }

    void UpdateUI()
    {
        uiGun.ForEach(i => i.UpdateValue(maxShoot, _currentShoots));
    }

    void GetAllUI()
    {
        uiGun = GameObject.FindObjectsByType<GunUI>(FindObjectsSortMode.None).ToList();
    }
}
