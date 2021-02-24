using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Forest
{
    public enum TileCollision
    {
        Passable = 0,
        Impassable = 1
    }

    public class Tile
    {
        public TileCollision Collision;
        public Rectangle TextureClip;

        private SpriteSheet _spriteSheet;
        private Animation _animation;
        private int _sprite;
        private Vector2 _position;

        public Tile(SpriteSheet spriteSheet, int sprite, TileCollision tileCollision, Vector2 position, Animation animation = null)
        {
            _spriteSheet = spriteSheet;
            _sprite = sprite;
            _position = position;
            _animation = animation;
            Collision = tileCollision;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _spriteSheet.DrawSprite(spriteBatch, _position, _sprite);
        }
    }
}
