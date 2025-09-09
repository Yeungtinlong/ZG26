namespace MBF.UnitBehaviors
{
    public interface IBeAttacked
    {
        public bool IsDead { get; }
        public int Hp { get; }
        public void GetDamage(int damage);
    }
}