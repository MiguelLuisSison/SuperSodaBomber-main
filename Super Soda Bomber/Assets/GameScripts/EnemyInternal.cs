
namespace SuperSodaBomber.Enemies{
    /// /// <summary>
    /// Interface for the Concrete Enemy Outer Class
    /// </summary>
    public interface IEnemyOuter{
        EnemyState GetState();
        void Flip();
    }

    /// <summary>
    /// Interface for the Concrete Enemy Inner Class
    /// </summary>
    public interface IEnemyInner{
        void CallState();
    }

    public enum EnemyType{
        Shooter, Roller
    }

    public enum EnemyPhase{
        Phase1, Phase2
    }

    public enum EnemyState{
        Wander, Chase, Attack
    }
}