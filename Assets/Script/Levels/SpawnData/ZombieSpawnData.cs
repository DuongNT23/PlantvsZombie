namespace Assets.Script.Levels.SpawnData
{
    [System.Serializable]
    public class ZombieSpawnData
    {
        public string Type { get; set; }
        public int Amount { get; set; }
        public string Row { get; set; }
        public bool Spread { get; set; }
    }
}
