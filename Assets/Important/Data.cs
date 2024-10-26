namespace Important
{
    public class Data
    {

        private float _skulls;
        public float GetSkulls() => _skulls;
        
        public Data()
        {
            _skulls = 0;
        }

        public void AddSkull() => _skulls += 1;

    }
}