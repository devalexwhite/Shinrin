using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Forest
{
    public class Camera
    {
        public Vector2 Position;
        public float Zoom;
        public float Rotation;

        public int ViewportWidth;
        public int ViewportHeight;

        private float Velocity = 0.0f;
        private const float Acceleration = 20.0f;
        private const float TopSpeed = 80.0f;
        private Vector2 movement;

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
                    Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                    Matrix.CreateTranslation(new Vector3(ViewportCenter, 0));
            }
        }

        public Camera(int viewportWidth, int viewportHeight, Vector2 cameraPosition)
        {
            ViewportWidth = viewportWidth;
            ViewportHeight = viewportHeight;
            Position = cameraPosition;
            Zoom = 2.0f;
        }

        public void AdjustZoom(float amount)
        {
            Zoom += amount;
            if (Zoom < 0.1f)
            {
                Zoom = 0.1f;
            }
        }

        public void MoveCamera(Vector2 cameraMovement, bool clampToMap = false)
        {
            Position += cameraMovement;
        }

        public void CenterOn(Vector2 position)
        {
            Position = position;
        }

        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return Vector2.Transform(worldPosition, TranslationMatrix);
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, Matrix.Invert(TranslationMatrix));
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            movement = new Vector2(0, 0);

            if (keyboardState.IsKeyDown(Keys.W))
            {
                movement.Y = -1;
            }
            else if (keyboardState.IsKeyDown(Keys.S))
            {
                movement.Y = 1;
            }

            else if (keyboardState.IsKeyDown(Keys.A))
            {
                movement.X = -1;
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                movement.X = 1;
            }

            if (!keyboardState.IsKeyDown(Keys.W) && !keyboardState.IsKeyDown(Keys.S) && !keyboardState.IsKeyDown(Keys.D) && !keyboardState.IsKeyDown(Keys.A))
            {
                Velocity = 0;
            }
            else
            {
                Velocity = Math.Clamp(Acceleration + Velocity, 0, TopSpeed);
            }


            MoveCamera(new Vector2(Velocity * movement.X * elapsed, Velocity * movement.Y * elapsed));
        }
    }
}