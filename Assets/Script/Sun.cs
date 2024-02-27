using UnityEngine;

namespace Assets.Script
{
    public class Sun : MonoBehaviour
    {
        public float dropTpYPos;
        private float speed = .15f;

        [SerializeField] private AudioClip _sunCollectClip;
        // Start is called before the first frame update
        void Start()
        {
            //   transform.position = new Vector3(Random.Range(-4f, 8.35f), 6, 0);
            //   dropTpYPos = Random.Range(2f, -3f);
            Destroy(gameObject, Random.Range(6f ,12f));
        }

        private void Update()
        {
            if (transform.position.y > dropTpYPos)
            {
                transform.position -= new Vector3(0, speed * Time.fixedDeltaTime, 0);
            }

        }

    }
}
