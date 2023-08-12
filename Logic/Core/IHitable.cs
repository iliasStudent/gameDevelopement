using Logic.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Core
{
    public interface IHitable : ICollisionable
    {
        public Stats stats { get; set; }
        public double invisibleTimer { get; set; }
        public bool invisible { get; set; }
        public void Hit(int damage);
    }
}
