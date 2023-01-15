namespace Architecture.Services.General {
    public interface IRandomService {
        public float Range(float min, float max);
        public int Range(int min, int max);
        public int Int();
        public float Float();
    }
}
