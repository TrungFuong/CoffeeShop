namespace CoffeeShop.Exceptions
{
    public sealed class NotFoundException : Exception
    {
        public NotFoundException(Type entityType, Guid id) 
            : base($"The {entityType.Name.ToLower()} with id: {id} doesn't exist in the database.")
            {
            } 
    }
}