using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityShoot : PlayerAbilityBase
{

    public List<GunBase> guns;
    public Transform gunPosition;
    private GunBase _currentGun;
    private int _currentGunIndex = -1;
    public VFXFlashColor flashColor;

    protected override void Init()
    {
        base.Init();
        EquipGun(0);
        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipGun(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) EquipGun(1);
    }

    void EquipGun(int index)
    {
        if (index < 0 || index >= guns.Count) return;
        if (index == _currentGunIndex) return;

        if (_currentGun != null)
        {
            _currentGun.StopShoot();
            Destroy(_currentGun.gameObject);
        }

        _currentGun = Instantiate(guns[index], gunPosition);
        _currentGun.transform.localPosition = Vector3.zero;
        _currentGun.transform.localEulerAngles = Vector3.zero;
        _currentGunIndex = index;
    }


    private void StartShoot()
    {
        _currentGun.StartShoot();
        flashColor?.Flash();
        Debug.Log("Shoot");
    }
    private void CancelShoot()
    {
        _currentGun.StopShoot();
        Debug.Log("Cancel Shoot");
    }
}
