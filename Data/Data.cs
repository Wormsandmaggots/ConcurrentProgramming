using Data;

namespace Data
{
    public abstract class AbstractDataApi
    {
        public abstract Ball CreateBall(int x, int y, int radius);

        public static AbstractDataApi CreateDataApi()
        {
            return new DataApi();
        }

        internal sealed class DataApi : AbstractDataApi
        {
            public override Ball CreateBall(int x, int y, int radius)
            {
                return new Ball(x, y, radius);
            }

        }
    }
}