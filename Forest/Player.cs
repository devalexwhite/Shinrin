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

    private const float Speed = 80.0f;
    private Vector2 movement;

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

      Animation toPlay = walkingUp;

      if (keyboardState.IsKeyDown(Keys.W))
      {
        movement.Y = -1;
        toPlay = walkingUp;
      }
      else if (keyboardState.IsKeyDown(Keys.S))
      {
        movement.Y = 1;
        toPlay = walkingDown;
      }

      if (keyboardState.IsKeyDown(Keys.A))
      {
        movement.X = -1;
        toPlay = walkingLeft;
      }
      else if (keyboardState.IsKeyDown(Keys.D))
      {
        movement.X = 1;
        toPlay = walkingRight;
      }

      if (!keyboardState.IsKeyDown(Keys.W) && !keyboardState.IsKeyDown(Keys.S) && !keyboardState.IsKeyDown(Keys.D) && !keyboardState.IsKeyDown(Keys.A))
      {
        animationPlayer.Pause();
      }
      else animationPlayer.Play(toPlay);


      Position.X += Speed * movement.X * elapsed;
      Position.Y += Speed * movement.Y * elapsed;

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
