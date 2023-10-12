namespace DogsHouse.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, int id) 
            : base($"Entity {name} (id: {id}) not found.") { }
    }
}
