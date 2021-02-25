using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Forest
{
  public class Player
  {
    public Vector2 Position;

    public const int Width = 32;
    public const int Height = 32;

    private float Velocity = 0.0f;
    private const float Acceleration = 20.0f;
    private const float TopSpeed = 80.0f;
    private Vector2 movement;
    private Texture2D Texture;

    Animation walkingUp, walkingRight, walkingDown, walkingLeft;
    AnimationPlayer animationPlayer;

    public Player(Vector2 position, ContentManager contentManager)
    {
      Position = position;

      LoadContent(contentManager);
    }

    public void Update(GameTime gameTime, KeyboardState keyboardState)
    {
      float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
      movement = new Vector2(0, 0);

      if (keyboardState.IsKeyDown(Keys.W))
      {
        movement.Y = -1;
        animationPlayer.Play(walkingUp);
      }
      else if (keyboardState.IsKeyDown(Keys.S))
      {
        movement.Y = 1;
        animationPlayer.Play(walkingDown);
      }

      else if (keyboardState.IsKeyDown(Keys.A))
      {
        movement.X = -1;
        animationPlayer.Play(walkingLeft);
      }
      else if (keyboardState.IsKeyDown(Keys.D))
      {
        movement.X = 1;
        animationPlayer.Play(walkingRight);
      }
      else
      {
        animationPlayer.Pause();
      }

      if (!keyboardState.IsKeyDown(Keys.W) && !keyboardState.IsKeyDown(Keys.S) && !keyboardState.IsKeyDown(Keys.D) && !keyboardState.IsKeyDown(Keys.A))
      {
        Velocity = 0;
      }
      else
      {
        Velocity = Math.Clamp(Acceleration + Velocity, 0, TopSpeed);
      }

      Position.X += Velocity * movement.X * elapsed;
      Position.Y += Velocity * movement.Y * elapsed;

    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
      animationPlayer.Draw(spriteBatch, gameTime, Position);
    }

    private void LoadContent(ContentManager contentManager)
    {
      Texture2D texture = contentManager.Load<Texture2D>("ranger_f");
      SpriteSheet spriteSheet = new SpriteSheet(texture, 32, 36);

      walkingDown = new Animation(spriteSheet, 7, 9);
      walkingRight = new Animation(spriteSheet, 4, 6);
      walkingLeft = new Animation(spriteSheet, 10, 12);
      walkingUp = new Animation(spriteSheet, 1, 3);

      animationPlayer.Play(walkingDown);
      animationPlayer.Pause();
    }
  }
}
