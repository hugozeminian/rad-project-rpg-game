using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carrot_game
{
    /// <summary>
    /// Entities are 'living' things. Our character and monster classes will inherit this. Entities have: Name, Health Points, Experience Points, Attack, Speed, Defense and SpriteImage. They can Attack(), Move(), Die()
    /// </summary>
    abstract class Entity
    {
        //Entities are abstract classes as we won't have any instantiated Entities. What we will have are monsters and a player, which must implement this abstract class.
    }
}
