
namespace Data
{
    public abstract class AbstractDataApi
    {
        public abstract IBall CreateBall(int x, int y, int radius);

        public static AbstractDataApi CreateDataApi()
        {
            return new DataApi();
        }

        internal sealed class DataApi : AbstractDataApi
        {
            public override IBall CreateBall(int x, int y, int radius)
            {
                return new Ball(x, y, radius);
            }

        }
    }
}