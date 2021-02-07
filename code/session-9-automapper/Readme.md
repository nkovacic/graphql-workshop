# Demostration of Sql Server, EF Core 5 and Automapper Bug with Hot Chocolate

# Query With Error..

Add id, to the session causes an error.
Changing session.id to int? fixes issue but not ideal
Returning session.name works as expected

1) Run update-databse in package manager console to create and seed sql server db
2) Run Project
3) Run below query which will generate error

```
{
  speakers{
    nodes{
      id
      sessionSpeakers{
        session{
          id
        }
      }
    }
  }
}
```

#Ideal Setup with AutoMapper and HotChocolate

```
	[UseApplicationDbContext]
	[UsePaging]
	[UseProjection]
	[UseSorting]
	public IQueryable<SpeakerDto> GetSpeakers(
	  [ScopedService] ApplicationDbContext context,
		[Service] IMapper mapper)
	{

		var query = context.Speakers
			   .ProjectTo<SpeakerDto>(mapper.ConfigurationProvider);

		var expression = query.Expression;

		return query;
	}
```
