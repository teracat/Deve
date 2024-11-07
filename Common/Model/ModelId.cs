namespace Deve
{
    public class ModelId
    {
        public long Id { get; set; } = 0;

        public ModelId()
        {
        }

        public ModelId(ModelId other)
        {
            Id = other.Id;
        }
    }
}
