namespace labvork_1_csharp.Services.Interfaces
{
    public interface IRelationshipManager<TEntity, TRelatedEntity>
    {
        bool AddRelationship(int entityID, int relatedEntityID);
        bool RemoveRelationship(int entityID, int relatedEntityID);
        IEnumerable<TRelatedEntity> GetRelatedEntities(int entityID);
    }
}