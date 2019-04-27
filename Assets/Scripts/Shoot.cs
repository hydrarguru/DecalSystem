using UnityEngine;

public class Shoot : MonoBehaviour
{
    ObjectPool objectPool;

    void Start()
    {
        //Gets object pool instance
        objectPool = ObjectPool.Instance;
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    void Fire()
    {
        //A bullet hole decal will spawn at the location/postition the mouse aimed.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        
        if(Physics.Raycast(ray, out hitInfo, 100f))
        {
            var wallDistance = .01f * hitInfo.normal;
            objectPool.SpawnFromPool("Bullet Hole", hitInfo.point + wallDistance, Quaternion.LookRotation(hitInfo.normal));
        }
    }
}
