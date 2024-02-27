using UnityEngine;

namespace Assets.Script.Plants
{
    public class SunFlower : MonoBehaviour
    {
        public GameObject sunObject;

        public float cooldown;

        private void Start()
        {
            InvokeRepeating("SpawnSun", cooldown, cooldown);
        }

        void SpawnSun()
        {
            GameObject mySun =  Instantiate(sunObject, new Vector3(transform.position.x, transform.position.y, 0 ), Quaternion.identity);
            mySun.GetComponent<Sun>().dropTpYPos = transform.position.y  - 1 ;
        }
    }
}
