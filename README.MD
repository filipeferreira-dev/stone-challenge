
# Running Api

1. Run [database.sql](https://github.com/filipeferreira-dev/stone-challenge/blob/master/database.sql) to create the database.

2. Change connection settings at [Presentation.api/appsetting.json](https://github.com/filipeferreira-dev/stone-challenge/blob/master/Presentation.Api/appsettings.json)

3. Just run on your visual studio or run 
```
dotnet run --project Presentation.Api/Presentation.Api.csproj 
```

# Running Monitor

1. Run the Api

2. Check the Api Uri in [Monitor/Monitor/settings.json](https://github.com/filipeferreira-dev/stone-challenge/blob/master/Monitor/Monitor/settings.json)

3. Just run on your visual studio or run 

```
dotnet run --project Monitor/Monitor/Monitor.csproj
```

You could set the interval (ms) in [settings.json](https://github.com/filipeferreira-dev/stone-challenge/blob/master/Monitor/Monitor/settings.json) too.

```
{
    "Interval" : "60000"
}
```
