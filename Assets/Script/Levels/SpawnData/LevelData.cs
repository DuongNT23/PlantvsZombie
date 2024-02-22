using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Levels.SpawnData
{
    [Serializable]
    public class LevelData
    {
        public int plantLimit = 9;
        public int startingSun = 50;
        public List<WaveSpawnData> waves;
    }
}
