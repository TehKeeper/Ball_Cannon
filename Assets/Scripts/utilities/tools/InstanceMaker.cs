namespace utilities.tools
{
    public class InstanceMaker<T> where T : class, new()
    {
        public T instance;

        public T I
        {
            get
            {
                if (instance != null)
                    return instance;

                return instance = new T();
            }
        }
    }
}