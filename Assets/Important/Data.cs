namespace Important
{
    public class Data
    {

        private int _skulls;
        public int GetSkulls() => _skulls;
        private int _bones;
        public int GetBones() => _bones;
        private int _fishes;
        public int GetFishes() => _fishes;
        private int _chests;
        public int GetChests() => _chests;
        
        public Data()
        {
            _skulls = 0;
            _bones = 0;
            _fishes = 0;
            _chests = 0;
        }

        public void AddSkull() => _skulls += 1;
        public void AddBone() => _bones += 1;
        public void AddFish() => _fishes += 1;
        public void AddChest() => _chests += 1;

    }
}