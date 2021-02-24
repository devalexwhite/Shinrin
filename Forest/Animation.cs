using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Forest
{
    public class Animation
    {
        private SpriteSheet _spriteSheet;
        private float _speed;
        private int _curFrame;
        private int _startFrame;
        private int _endFrame;
        private double _start;
        private bool _paused = false;

        public Animation(SpriteSheet spriteSheet, int startFrame, int endFrame)
        {
            _spriteSheet = spriteSheet;
            _startFrame = startFrame;
            _endFrame = endFrame;
        }

        public void Pause()
        {
            _paused = true;
        }

        public void Resume()
        {
            _paused = false;
        }

        public void Play(float speed = 300.0f)
        {
            _speed = speed;
            _paused = false;
            _curFrame = _startFrame;
            _start = 0;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 destination)
        {
            if (_start == 0) _start = gameTime.TotalGameTime.TotalMilliseconds;

            double elapsed = gameTime.TotalGameTime.TotalMilliseconds - _start;

            if (elapsed > _speed && !_paused)
            {
                _curFrame++;
                if (_curFrame > _endFrame) _curFrame = _startFrame;

                _start = gameTime.TotalGameTime.TotalMilliseconds;
            }

            _spriteSheet.DrawSprite(spriteBatch, destination, _curFrame);
        }
    }
}
