using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Forest
{
    public class SpriteSheet
    {
        public Texture2D _texture2D;
        public int _spriteCount;

        private int _row;
        private int _spriteWidth;
        private int _spriteHeight;
        private int _perRow;
        private int _rows;

        public SpriteSheet(Texture2D texture2D, int spriteWidth, int spriteHeight)
        {
            _texture2D = texture2D;

            _perRow = texture2D.Width / spriteWidth;
            _rows = texture2D.Height / spriteHeight;
            _spriteCount = _perRow * _rows;

            _spriteWidth = spriteWidth;
            _spriteHeight = spriteHeight;
        }

        public Vector2 GetSpriteSize()
        {
            return new Vector2(_spriteWidth, _spriteHeight);
        }

        public void DrawSprite(SpriteBatch spriteBatch, Vector2 destination, int spriteNumber)
        {
            if (spriteNumber > _spriteCount || spriteNumber < 0) throw new Exception("Specified sprite does not exist");


            spriteBatch.Draw(_texture2D, destination, GetSpriteSource(spriteNumber), Color.White);
        }

        private Rectangle GetSpriteSource(int spriteNumber)
        {
            int row = (int)(spriteNumber - 1) / _perRow;
            int col = (spriteNumber - 1) % _perRow;

            return new Rectangle(col * _spriteWidth, row * _spriteHeight, _spriteWidth, _spriteHeight);
        }
    }
}
