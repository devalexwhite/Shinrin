using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Forest
{
  public class Camera
  {
    public Vector2 Position
    {
      get { return position; }
    }
    Vector2 position;

    public Vector2 Destination
    {
      get { return destination; }
    }
    Vector2 destination;

    public float Zoom
    {
      get { return zoom; }
    }
    float zoom;

    public float Rotation
    {
      get { return rotation; }
    }
    float rotation;

    public int ViewportWidth
    {
      get { return viewportWidth; }
    }
    int viewportWidth;

    public Rectangle WorldBounds
    {
      get { return worldBounds; }
      set { worldBounds = value; }
    }
    Rectangle worldBounds;

    public int ViewportHeight
    {
      get { return viewportHeight; }
    }
    int viewportHeight;

    public Vector2 ViewportCenter
    {
      get
      {
        return new Vector2(ViewportWidth * 0.5f, ViewportHeight * 0.5f);
      }
    }

    public Matrix TranslationMatrix
    {
      get
      {
        return Matrix.CreateTranslation(-(int)Position.X, -(int)Position.Y, 0) *
            Matrix.CreateRotationZ(Rotation) *
            Matrix.CreateScale(new Vector3(Zoom, Zoom, 1));
      }
    }

    private const float speed = 1.2f;

    public Camera(int viewportWidth, int viewportHeight, Vector2 cameraPosition, Rectangle worldBounds = new Rectangle())
    {
      this.viewportWidth = viewportWidth;
      this.viewportHeight = viewportHeight;
      this.position = cameraPosition;
      this.zoom = 1.0f;
      this.worldBounds = worldBounds;
    }

    public void AdjustZoom(float amount)
    {
      zoom += amount;
      if (zoom < 0.1f)
      {
        zoom = 0.1f;
      }
    }

    public void MoveCamera(Vector2 cameraMovement)
    {
      position += cameraMovement;
    }

    public void SetDestination(Vector2 destination)
    {
      this.destination = Vector2.Clamp(destination, ViewportCenter, new Vector2(worldBounds.Right - ViewportCenter.X, worldBounds.Bottom - ViewportCenter.Y));
    }

    public void CenterOn(Vector2 position)
    {
      this.position = position;
    }

    public Vector2 WorldToScreen(Vector2 worldPosition)
    {
      return Vector2.Transform(worldPosition, TranslationMatrix);
    }

    public Vector2 ScreenToWorld(Vector2 screenPosition)
    {
      return Vector2.Transform(screenPosition, Matrix.Invert(TranslationMatrix));
    }

    public void Update(GameTime gameTime)
    {
      Vector2 centerPosition = Position + ViewportCenter;
      if (centerPosition == Destination) return;

      Vector2 posDiff = Destination - centerPosition;

      Vector2 movement = new Vector2(0, 0);

      if (Math.Abs(posDiff.X) < speed) movement.X = posDiff.X;
      else if (posDiff.X != 0) movement.X = posDiff.X > 0 ? speed : -speed;

      if (Math.Abs(posDiff.Y) < speed) movement.Y = posDiff.Y;
      else if (posDiff.Y != 0) movement.Y = posDiff.Y > 0 ? speed : -speed;

      MoveCamera(movement);
    }
  }
}