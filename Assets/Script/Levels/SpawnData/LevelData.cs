using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Levels.SpawnData
{
    public class LevelData
    {
        public IEnumerable<WaveSpawnData> Waves { get; set; }
    }
}
