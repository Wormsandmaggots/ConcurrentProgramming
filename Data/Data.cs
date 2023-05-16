namespace Data
{
    public abstract class AbstractDataApi
    {
        public abstract IBall CreateBall(int x, int y, int width, int height);

        public static AbstractDataApi CreateDataApi()
        {
            return new DataApi();
        }

        internal sealed class DataApi : AbstractDataApi
        {
            public override IBall CreateBall(int x, int y, int width, int height)
            {
                return new Ball(x, y, width, height);
            }
        }
    }
}