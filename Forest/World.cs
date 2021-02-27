using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Forest
{
  public class World : IDisposable
  {
    public Rectangle WorldBounds
    {
      get
      {
        return new Rectangle(0, 0, worldWidth * 16, worldHeight * 16);
      }
    }

    public List<Tile[,]> Layers
    {
      get { return layers; }
    }
    List<Tile[,]> layers = new List<Tile[,]>();

    public int WorldWidth
    {
      get { return worldWidth; }
    }
    int worldWidth;

    public int WorldHeight
    {
      get { return worldHeight; }
    }
    int worldHeight;

    public Player Player
    {
      get { return player; }
    }
    Player player;

    public Camera Camera
    {
      get { return camera; }
    }
    Camera camera;

    public SpriteSheet SpriteSheet
    {
      get { return spriteSheet; }
    }
    SpriteSheet spriteSheet;

    public Vector2 StartingPosition
    {
      get { return startingPosition; }
    }
    private Vector2 startingPosition;

    public ContentManager Content
    {
      get { return contentManager; }
    }
    ContentManager contentManager;

    public World(IServiceProvider serviceProvider, GameWindow window)
    {

      contentManager = new ContentManager(serviceProvider, "Content");

      startingPosition = new Vector2(200, 200);

      camera = new Camera(window.ClientBounds.Width, window.ClientBounds.Height, startingPosition);

      Initalize();
    }

    public void Dispose()
    { }

    public Matrix GetTranslationMatrix()
    {
      return camera.TranslationMatrix;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      foreach (Tile[,] layer in layers)
      {
        foreach (Tile tile in layer)
        {
          if (tile != null) tile.Draw(gameTime, spriteBatch);
        }
      }


      player.Draw(spriteBatch, gameTime);
    }

    public void Update(GameTime gameTime, KeyboardState keyboardState)
    {
      player.Update(gameTime, keyboardState);

      camera.SetDestination(player.Position);

      camera.Update(gameTime);
    }

    private void Initalize()
    {
      player = new Player(camera.ScreenToWorld((camera.ViewportCenter - new Vector2(16, 16))), contentManager);

      Texture2D worldTexture = contentManager.Load<Texture2D>("16tiles");
      spriteSheet = new SpriteSheet(worldTexture, 16, 16);

      using (Stream fileStream = TitleContainer.OpenStream("Content/worldmap_Tile Layer 1.csv"))
        layers.Add(LoadLayer(fileStream));

      using (Stream fileStream = TitleContainer.OpenStream("Content/worldmap_Tile Layer 2.csv"))
        layers.Add(LoadLayer(fileStream));

      using (Stream fileStream = TitleContainer.OpenStream("Content/worldmap_Tile Layer 3.csv"))
        layers.Add(LoadLayer(fileStream));

      camera.WorldBounds = WorldBounds;
    }

    private Tile[,] LoadLayer(Stream fileStream)
    {
      List<string> lines = new List<string>();
      using StreamReader reader = new StreamReader(fileStream);
      string line = reader.ReadLine();

      worldWidth = line.Split(',').Length;

      while (line != null) { lines.Add(line); line = reader.ReadLine(); }

      worldHeight = lines.Count;

      Tile[,] layer = new Tile[worldWidth, worldHeight];

      for (int y = 0; y < worldHeight; y++)
      {
        string[] csvLine = lines[y].Split(',');
        for (int x = 0; x < worldWidth; x++)
        {
          int spriteId = int.Parse(csvLine[x]);
          if (spriteId >= 0) layer[x, y] = LoadTile(spriteId, x, y);
        }
      }

      return layer;
    }

    private Tile LoadTile(int spriteID, int x, int y)
    {
      if (spriteID < 0) throw new Exception("Invalid ID");

      Vector2 position = new Vector2(x, y) * (spriteSheet.GetSpriteSize());

      return new Tile(spriteSheet, spriteID + 1, TileCollision.Passable, position);
    }
  }
}
