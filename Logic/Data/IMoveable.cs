using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Data
{
    public interface IMoveable : IPositionable
    {
        public Movement Movement { get; set; }
    }
}
