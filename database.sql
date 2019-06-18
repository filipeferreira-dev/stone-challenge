if  not exists (select top 1 1 from sys.databases where [name] = 'st') 
begin 
    create database ST;  
    use ST; 
    
    create table City 
    (
        [Key] uniqueidentifier primary key,
        [Name] varchar(500) not null,
        [PostalCode] varchar(9) not null,
        [CreatedOn] datetime not null,
        [DeletedAt] datetime null
    )
END
