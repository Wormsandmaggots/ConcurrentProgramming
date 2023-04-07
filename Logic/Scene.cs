using System;
using Data;

namespace Logic
{
    internal class Scene
    {
        private int _width;
        private int _height;
        private List<BallLogic> _balls = new List<BallLogic>();

        public Scene(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public void GenerateBallsList(int ballsAmount, int ballsRadius)
        {
            _balls.Clear();

            Random r = new Random();

            for (int i = 0; i < ballsAmount; i++)
            {
                int x = r.Next(ballsRadius, _width - ballsRadius);
                int y = r.Next(ballsRadius, _height - ballsRadius);

                _balls.Add(new BallLogic(x, y, ballsRadius));
            }

        }

        public int Width => _width;
        public int Height => _height;
        public List<BallLogic> Balls => _balls;
    }
}
