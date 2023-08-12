using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Core
{
    public class Stats
    {
        public int health { get; set; }
        public int maxHealth { get; set; }
        public int damage { get; set; }

        public Stats(int health, int damage)
        {
            this.maxHealth = health;
            this.health = health;
            this.damage = damage;
        }

        public Stats(int health, int maxHealth, int damage)
        {
            this.health = health;
            this.maxHealth = maxHealth;
            this.damage = damage;
        }
    }
}
