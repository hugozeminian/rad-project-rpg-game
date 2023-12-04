using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carrot_game
{
    internal class CollisionChecker
    {
    public GameScreen gs;

        public CollisionChecker(GameScreen gs)
        {
            this.gs = gs;
        }
        public void CheckCollision(Entity e)
        {
            int leftTileColumn = (e.WorldX - e.ScreenX + e.BoundingBox.Left) / MapTile.tileSize;
            int rightTileColumn = (e.WorldX - e.ScreenX + e.BoundingBox.Right) / MapTile.tileSize;
            int upTileRow = (e.WorldY - e.ScreenY + e.BoundingBox.Top) / MapTile.tileSize;
            int downTileRow = (e.WorldY - e.ScreenY + e.BoundingBox.Bottom) / MapTile.tileSize;

            int tile1, tile2;

            switch(e.Direction)
            {
                case "up":
                    upTileRow = (upTileRow * MapTile.tileSize - e.Speed) / MapTile.tileSize;
                    tile1 = gs.gameMap.mapData[upTileRow,leftTileColumn];
                    tile2 = gs.gameMap.mapData[upTileRow,rightTileColumn];
                    if (MapTile.TileSet[tile1].collision == true || MapTile.TileSet[tile2].collision == true)
                        e.isColliding = true;
                    break;
                case "down":
                    downTileRow = (downTileRow * MapTile.tileSize + e.Speed) / MapTile.tileSize;
                    tile1 = gs.gameMap.mapData[downTileRow, leftTileColumn];
                    tile2 = gs.gameMap.mapData[downTileRow, rightTileColumn];
                    if (MapTile.TileSet[tile1].collision == true || MapTile.TileSet[tile2].collision == true)
                        e.isColliding = true;
                    break;
                case "left":
                    leftTileColumn = (leftTileColumn * MapTile.tileSize - e.Speed) / MapTile.tileSize;
                    tile1 = gs.gameMap.mapData[upTileRow, leftTileColumn];
                    tile2 = gs.gameMap.mapData[downTileRow, leftTileColumn];
                    if (MapTile.TileSet[tile1].collision == true || MapTile.TileSet[tile2].collision == true)
                        e.isColliding = true;
                    break;
                case "right":
                    rightTileColumn = (rightTileColumn * MapTile.tileSize + e.Speed) / MapTile.tileSize;
                    tile1 = gs.gameMap.mapData[upTileRow, rightTileColumn];
                    tile2 = gs.gameMap.mapData[downTileRow, rightTileColumn];
                    if (MapTile.TileSet[tile1].collision == true || MapTile.TileSet[tile2].collision == true)
                        e.isColliding = true;
                    break;
            }
        }
 
    }
}
