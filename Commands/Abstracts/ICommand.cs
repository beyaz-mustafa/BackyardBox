namespace Commands.Abstracts
{
    public interface ICommand<T>
    {
        public T Find(int id);
    }
}
