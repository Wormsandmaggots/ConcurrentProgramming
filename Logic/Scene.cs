namespace Logic
{
    internal class Scene
    {
        private int _width;
        private int _height;
        private List<IBallLogic> _balls = new List<IBallLogic>();
        private bool _enabled;

        public Scene(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public void GenerateBallsList(int ballsAmount, int ballsRadius)
        {
            foreach(IBallLogic ball in _balls)
            {
                ball.Dispose();
            }

            _balls.Clear();

            Random r = new Random();

            AbstractLogicApi api = AbstractLogicApi.CreateApi();

            for (int i = 0; i < ballsAmount; i++)
            {
                int x = r.Next(ballsRadius, _width - ballsRadius);
                int y = r.Next(ballsRadius, _height - ballsRadius);

                _balls.Add(api.CreateBall(x,y, ballsRadius, _width, _height));
            }

            Enabled = true;
        }

        public int Width => _width;
        public int Height => _height;
        public List<IBallLogic> Balls => _balls;
        public bool Enabled
        {
            get { return _enabled; }
            set
            {

                _enabled = value;

                foreach(IBallLogic b in _balls)
                {
                    b.ToggleBall(value);
                }
            }
        }
    }
}
