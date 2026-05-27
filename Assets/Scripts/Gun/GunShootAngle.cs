using UnityEngine;

public class GunShootAngle : GunShootLimit
{
    public int amountPerShoot = 4;
    public float angle = 15f;

    public override void Shoot()
    {
        for (int i = 0; i < amountPerShoot; i++)
        {
            int pair = i / 2 + 1;
            float side = (i % 2 == 0) ? 1f : -1f;
            SpawnProjectile(angle * side * pair);
        }
    }

    private void SpawnProjectile(float yawOffset)
    {
        var projectile = Instantiate(prefabProjectile, positionToShoot);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.localEulerAngles = Vector3.up * yawOffset;
        projectile.speed = speed;
        projectile.transform.parent = null;
    }
}
