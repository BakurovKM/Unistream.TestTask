using Microsoft.EntityFrameworkCore;
using Unistream.TestTask.Db;
using Unistream.TestTask.Db.Domain;

namespace Unistream.TestTask.Host.Routes;

public static class MapRoutes
{
    public static void MapEntitiesRoutes(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/entity/{id:guid}", Get);
        builder.MapPost("/entity", Post);
    }

    private static async Task<IResult> Get(Guid id, EntitiesContext context, CancellationToken cancellationToken)
    {
        var entity = await context.Entities
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
        
        return entity == null 
            ? Results.NotFound() 
            : Results.Ok(new EntityModel(entity.Id, entity.OperationDate, entity.Amount));
    }
    
    private static async Task<IResult> Post(EntityModel entity, EntitiesContext context, CancellationToken cancellationToken)
    {
        context.Entities
            .Add(new Entity(entity.Id)
            {
                Amount = entity.Amount,
                OperationDate = entity.OperationDate
            });
        await context.SaveChangesAsync(cancellationToken);
        
        return Results.Created($"/entity/{entity.Id}", null);
    }
}

public record struct EntityModel(Guid Id, DateTime OperationDate, decimal Amount);