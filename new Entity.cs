using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace ActionGame
{
    class Entity : DynamicObject
    {
        public static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();

        public Vector2 PreviousPosition { get; protected set; }
        public float Health { get; protected set; }
        public float HealthMax { get; protected set; }
        public float HealthRegen { get; protected set; }
        public int Level { get; protected set; }
        public int Experience { get; protected set; }
        public int ExperienceMax { get; protected set; }
        public int Speed { get; protected set; }
        public bool canBeHealed { get; protected set; }
        public bool canBeDamaged { get; protected set; }
        public bool isMoving { get; protected set; }
        public bool isAttacking { get; protected set; }
        public bool isCasting { get; protected set; }
        public bool isAlive { get; protected set; }
        public bool canMove { get; set; }
        public bool canAttack { get; set; }
        public bool canCast { get; set; }
        float timeLapse { get; set; }
        

        public StatusEffectManager effectManager;
        public AbilityManager abilityManager;

        public Entity(string key) : base(Textures[key])
        {
            this.effectManager = new StatusEffectManager(new List<StatusEffect>(),this);
            this.abilityManager = new AbilityManager(this);
            //The size in pixels per tile.
            Speed = 256;
        }

       public void Collide(Projectile projectile, GameTime gameTime)
        {
            List<StatusEffect> effects = projectile.effectManager.effects;
            effectManager.Update(gameTime, false, this, projectile);
            if (!effectManager.hasReflect)
            {
                effectManager.AddEffect(effects);
            }

        }

        new public static void LoadContent(ContentManager Content)
       {

       }

        public void ChangeAttr(string attr, float value, Projectile damageSource)
        {
            attr.ToLower();
            switch (attr)
            {
                case "health":
                    if (canBeDamaged && value < 0)
                        Health += value;
                    else if (canBeHealed && value > 0)
                        Health += value;
                    break;
                case "healthmax":
                    HealthMax += value;
                    break;
                case "healthregen":
                    HealthRegen += value;
                    break;
                case "experience":
                    Experience += (int)value;
                    break;
                default:
                    break;
            }
            
            
        }

        new public void Update(GameTime gameTime)
        {
            
            timeLapse = ((float)gameTime.ElapsedGameTime.Milliseconds) / 1000.0f;
            ChangeAttr("health", HealthRegen * timeLapse, null );

            PreviousPosition = Position;
            isAttacking = false;
            isCasting = false; 

            base.Update(gameTime);

            // Update Logic

            abilityManager.Update(gameTime);

            effectManager.Update(gameTime, true, this, null);

            if (PreviousPosition != Position)
                isMoving = true;
            else
                isMoving = false;

        }
       
        public bool UseAbility(uint index)
        {
            if(canAttack && index == 0)
            {
                isAttacking = abilityManager.UseAbility(index);
                return true;
            }
            else if(canCast && index != 0)
            {
                isCasting = abilityManager.UseAbility(index);
                return true;
            }
            return false;
        }
    }
}
