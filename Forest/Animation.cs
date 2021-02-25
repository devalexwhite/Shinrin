using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Forest
{
  public class Animation
  {
    public SpriteSheet SpriteSheet
    {
      get { return spriteSheet; }
    }
    SpriteSheet spriteSheet;

    public float FrameTime
    {
      get { return frameTime; }
    }
    float frameTime;

    public bool ShouldLoop
    {
      get { return shouldLoop; }
    }
    bool shouldLoop;

    public int StartFrame
    {
      get { return startFrame; }
    }
    int startFrame;

    public int EndFrame
    {
      get { return endFrame; }
    }
    int endFrame;

    public Animation(SpriteSheet spriteSheet, int startFrame, int endFrame, float frameTime = 200.0f, bool shouldLoop = true)
    {
      this.startFrame = startFrame;
      this.endFrame = endFrame;
      this.spriteSheet = spriteSheet;
      this.frameTime = frameTime;
      this.shouldLoop = shouldLoop;
    }
  }
}
