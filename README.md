### Requisitos:

-Registro de usuario para crear un nuevo usuario.

-Punto de conexión de inicio de sesión para autenticar al usuario y generar un token.

-Operaciones CRUD para gestionar la lista de tareas.

-Implementar la autenticación de usuario para permitir que solo los usuarios autorizados accedan a la lista de tareas pendientes.(se usará JWT)

-Implementar medidas de seguridad y manejo de errores.

-Utilizar una base de datos para almacenar los datos del usuario y de la lista de tareas pendientes.

-Implementar una validación de datos.

-Implementar la paginación y el filtrado para la lista de tareas pendientes.

Tendremos dos entidades momentaneamente para la aplicacion, usuario y tarea.
Para la modelacion de la entidad usuario en la base de datos usaremos Identity con EntityFrameWork, luego la entidad tarea tendra los siguientes atributos: id, title, descripcion y estado(opciones: Pendiente, Realizada, Cancelada), ademas tendremos una relacion entre usuario y tarea, sera una relacion de 1 a muchos, donde un usuario puede tener muchas tareas, y una tarea solo le corresponde a un usuario.

A continuación se muestra una lista de los puntos finales y los detalles de la solicitud y la respuesta:

#### Registro de usuarios:

'''
POST /register
{
  "name": "User Name",
  "email": "user@name.com",
  "password": "password"
}
'''
Esto validará los detalles dados, se asegurará de que el correo electrónico sea único y almacenará los detalles del usuario en la base de datos. Responde con un token que se pueda usar para la autenticación si el registro se realiza correctamente.
'''
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9"
}
'''

#### Login de usuarios:

'''
POST /login
{
  "email": "user@name.com",
  "password": "password"
}
'''
Esto validará el correo electrónico y la contraseña dados, y responderá con un token si la autenticación se realiza correctamente.

#### Crear un To-Do item:

'''
POST /task
{
  "title": "Buy groceries",
  "description": "Buy milk, eggs, and bread"
}
'''

Si el usuario no esta autenticado se responderá con un 401
'''
{
  "message": "Unauthorized"
}
'''

En caso contrario se devuelve el nuevo item creado

'''
{
  "id": 1,
  "title": "Buy groceries",
  "description": "Buy milk, eggs, and bread",
  "state": "Pendiente"
}
'''

#### Actualizar un To-Do item:

'''
PUT /task/1
{
  "title": "Buy groceries",
  "description": "Buy milk, eggs, bread, and cheese"
}
'''
Se verifica que el usuario sea el creador de dicha tarea, si es así se actualiza el item, de lo contrario se devuelve un 403.
'''
{
  "message": "Forbidden"
}
'''
Si el usuario esta autorizado para actualizar el item se devuelve el item actualizado
'''
{
  "id": 1,
  "title": "Buy groceries",
  "description": "Buy milk, eggs, bread, and cheese",
  "state": "Pendiente"
}
'''

#### Eliminar un To-Do item:

DELETE /task/1

Se verifica que el usuario sea el creador de dicha tarea, si es así se elimina el item y se devuelve un 204, de lo contrario se devuelve un 403.


#### Get To-Do items:

GET /task?page=1&limit=10

Se devuelve las tareas planificadas por ese usuario usando paginacion, ademas se devuelven los detalles de pagina, limit y total.
'''
{
  "data": [
    {
      "id": 1,
      "title": "Buy groceries",
      "description": "Buy milk, eggs, bread",
      "state": "Pendiente"
    },
    {
      "id": 2,
      "title": "Pay bills",
      "description": "Pay electricity and water bills",
      "state": "Realizado"
    }
  ],
  "page": 1,
  "limit": 10,
  "total": 2
}
'''

Para agregar una nueva migracion desde la raiz del proyecto To-Do-List.Infrastructure ejecutar los
comandos:

- dotnet ef migrations add MigrationName --startup-project ../To-Do_List.Api --context AppDBContext
- dotnet ef database update --startup-project ../To-Do_List.Api --context AppDBContext
