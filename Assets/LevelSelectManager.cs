using System.Collections.Generic;
using System.Linq;
using Assets.Script.Save;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets
{
    public class LevelSelectManager : MonoBehaviour
    {
        [SerializeField] private GameObject levelList;
        private Color PassedColor = new Color(0.35f, 0.8f, 0.5f);

        // Start is called before the first frame update
        void Start()
        {
            SaveGameData data = SaveGameManager.Instance.LoadGame();
            var children = levelList.GetComponentsInChildren<LevelSelectButton>();
            foreach (var child in children)
            {
                var button = child.GetComponent<Button>();
                if (child.alwaysUnlocked
                    || data.IsLevelCompleted(child.levelId))
                {
                    button.interactable = true;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void UnlockAll()
        {
            SaveGameData data = SaveGameManager.Instance.LoadGame();
            data.UnlockedLevels = Enumerable.Range(0, 50).ToList();
            SaveGameManager.Instance.SaveGame(data);
            SceneManager.LoadScene("LevelSelectScreen");
        }
    
    }
}
