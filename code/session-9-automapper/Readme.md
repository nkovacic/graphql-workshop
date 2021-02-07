# Demostration of Sql Server, EF Core 5 and Automapper Bug with Hot Chocolate

# Query With Error..

Add id, to the session causes an error.
Changing session.id to int? fixes issue but not ideal
Returning session.name works as expected
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