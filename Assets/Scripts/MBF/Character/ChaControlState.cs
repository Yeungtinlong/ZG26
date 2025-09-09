namespace MBF
{
    public struct ChaControlState
    {
        public static readonly ChaControlState stun = new ChaControlState(false, false);
        public static readonly ChaControlState origin = new ChaControlState(true, true);

        public bool canMove;
        public bool canUseSkill;

        public ChaControlState(bool canMove, bool canUseSkill)
        {
            this.canMove = canMove;
            this.canUseSkill = canUseSkill;
        }

        public void Origin()
        {
            this.canMove = true;
            this.canUseSkill = true;
        }

        public static ChaControlState operator +(ChaControlState state1, ChaControlState state2)
        {
            return new ChaControlState(
                state1.canMove & state2.canMove,
                state1.canUseSkill & state2.canUseSkill
            );
        }
    }
}