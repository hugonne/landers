1. Agregar los Steps de cada ToDo en los métodos Get y GetById

2. Agregar un Step a un Todo

3. Borrar un step de un Todo

4. Completar un Step. Si al hacerlo todos los demás Steps de ese Todo están completados, completar el Todo

5. Completar un Todo. En este caso, se deben completar también todos sus Steps.

6. Llevar a Dtos las operaciones con Steps.

Actualizar la bd (desde la carpeta de DataAccess)
dotnet ef database update -s ../tareasapi

Agregar migración (desde la carpeta de DataAccess)
dotnet ef migrations add Nombre -s ../tareasapi

Instalar EF globalmente
dotnet tool install —-global dotnet-ef
