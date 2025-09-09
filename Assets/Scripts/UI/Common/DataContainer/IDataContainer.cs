namespace TheGame.UI
{
    public interface IDataContainer<in T>
    {
        public void SetData(T data);
    }
}