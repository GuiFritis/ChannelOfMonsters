using Utils;

public class CollectablesPool : PoolBase<Collectable, CollectablesPool>
{
    protected override Collectable CreatePoolItem()
    {
        var item = base.CreatePoolItem();
        item.Pool = this;
        return item;
    }
}
