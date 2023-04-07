using System;
using Data;

namespace Logic
{
    public class Scene
    {
        private int _width;
        private int _height;
        private List<Ball> _ballsList = new List<Ball>();

        public Scene(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public void GenerateBallsList(int ballsAmount, int ballsRadius)
        {
            _ballsList.Clear();

            Random r = new Random();

            for (int i = 0; i < ballsAmount; i++)
            {
                int x = r.Next(ballsRadius, _width - ballsRadius);
                int y = r.Next(ballsRadius, _height - ballsRadius);

                _ballsList.Add(new Ball(x, y, ballsRadius));
            }

        }

        public int Width => _width;
        public int Height => _height;
        public List<Ball> BallsList => _ballsList;
    }
}
