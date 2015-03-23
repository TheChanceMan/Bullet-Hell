using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Object
{
    abstract class Entity : DynamicObject
    {
        protected int health;
        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        protected int healthMax;
        public int HealthMax
        {
            get { return healthMax; }
            set { healthMax = value; }
        }
        protected int healthRegen;
        public int HealthRegen
        {
            get {  return healthRegen; }
            set { healthRegen = value; }
        }
        protected bool isalive;
        public bool isAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }
        //projectile
        protected int damage;
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }
        protected int level;
        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        public Entity(Texture2D texture) : base(texture)
        {
            this.Health = health;
            this.HealthMax = healthMax;
            this.HealthRegen = healthRegen;
            this.isalive = isAlive;
            this.Damage = damage;
            this.Level = level; 
        }
        public abstract void Attack(List<Entity> entities);

    }
}
