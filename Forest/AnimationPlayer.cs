using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Forest
{
  public struct AnimationPlayer
  {
    public Animation Animation
    {
      get { return animation; }
    }
    Animation animation;

    public int CurrentFrame
    {
      get { return currentFrame; }
    }
    int currentFrame;

    public float TimeSinceFrame
    {
      get { return timeSinceFrame; }
    }
    float timeSinceFrame;

    public bool IsPlaying
    {
      get { return isPlaying; }
    }
    bool isPlaying;

    public void Play(Animation animation)
    {
      if (this.animation == animation)
      {
        this.isPlaying = true;
        return;
      }

      this.animation = animation;

      this.currentFrame = this.animation.StartFrame;
      this.timeSinceFrame = 0;
      this.isPlaying = true;
    }

    public void Pause()
    {
      this.isPlaying = false;
    }

    public void Resume()
    {
      this.isPlaying = true;
    }

    public void Restart()
    {
      this.currentFrame = this.animation.StartFrame;
      this.timeSinceFrame = 0;
    }

    public void Stop()
    {
      this.isPlaying = false;
      this.currentFrame = 0;
      this.timeSinceFrame = 0;
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 position)
    {
      if (this.isPlaying)
      {
        this.timeSinceFrame += gameTime.ElapsedGameTime.Milliseconds;

        if (this.timeSinceFrame >= this.animation.FrameTime)
        {
          this.currentFrame++;
          this.timeSinceFrame = 0;
        }

        if (this.currentFrame > this.animation.EndFrame && this.animation.ShouldLoop) Restart();
        else if (this.currentFrame > this.animation.EndFrame) Stop();
      }

      this.animation.SpriteSheet.DrawSprite(spriteBatch, position, this.CurrentFrame);
    }
  }
}