if  not exists (select top 1 1 from sys.databases where [name] = 'st')
begin
  create database ST
  go 
  begin
    use ST;
  end

    create table City 
    (
      [Key] uniqueidentifier primary key,
      [Name] varchar(500) not null,
      [PostalCode] varchar(9) not null,
      [CreatedOn] datetime not null,
      [DeletedAt] datetime null
    );

    create table CityTemperature 
    (
      [key] uniqueidentifier constraint pk_city_temperature_key primary key,
      [CityKey] uniqueidentifier not null constraint fk_city_temperature_city_key foreign key references City([key]),
      [Temperature] int not null,
      [CreatedOn] datetime not null,
      [DeletedAt] datetime null
    );
end