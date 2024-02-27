using UnityEngine;

namespace Assets.Script.Save
{
    public class SaveGameManager : MonoBehaviour
    {
        public static SaveGameManager Instance;
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }


    }
}
