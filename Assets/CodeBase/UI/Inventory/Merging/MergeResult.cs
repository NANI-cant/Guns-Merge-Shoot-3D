namespace UI.Inventory.Merging {
    public readonly struct MergeResult{
        public MergeType MergeType { get; }
        public MergeWeapon Active { get; }
        public MergeWeapon Passive { get; }
        
        public MergeResult(MergeType mergeType, MergeWeapon active, MergeWeapon passive) {
            MergeType = mergeType;
            Active = active;
            Passive = passive;
        }
    }
}