Prueba técnica Full Stack
Instrucciones
Desarrolla un sistema de encuestas que permita crear y aplicar encuestas a usuarios finales
(encuestados) mediante una aplicación web.
En el sistema habrá un rol de administrador que podrá realizar las siguientes funciones:
- iniciar sesión (se debe validar usuario y contraseña)
  - se debe registrar un administrador por default
- registrar, actualizar y eliminar usuarios administradores con los siguientes datos:
  - Nombre
  - Apellido
  - Nombre de usuario
  - Contraseña
- Crear, actualizar y eliminar encuestas con los siguientes datos:
  - Nombre
  - Descripción
  - Fecha de registro
  - Fecha inicio
  - Fecha fin
- Crear, actualizar y eliminar preguntas a cada encuesta. Los datos de las preguntas son:
- Pregunta
- Tipo de pregunta
- Las preguntas pueden ser de dos tipos: Abierta y Opciones (n
opciones)
- Si la pregunta es tipo Opciones se debe permitir registrar las opciones
disponibles para dicha pregunta

- EL administrador puede especificar si una pregunta es obligatoria

- Visualizar encuestas y preguntas relacionadas
  - Mostrar listado con paginación de resultados
- Visualizar los resultados de las encuestas aplicadas mostrando los datos del encuestado
  - Mostrar listado con paginación de resultados

Para el proceso de contestar una encuesta se debe tener un listado público de las encuestas que estén
el rango de fechas inicio y fin con relación a la fecha actual, para poder contestar una encuesta el
usuario encuestado (rol usuario) debe registrarse con los siguientes datos:
- Nombre
- Apellido
- Nombre de usuario
- Contraseña
Una vez registrado se podrá iniciar sesión y seleccionar una encuesta para contestar.
Cuando se seleccione una encuesta se mostrarán las preguntas y un campo de texto para las preguntas
abiertas o las opciones para seleccionar, al terminar de contestar las preguntas se guardará el
resultado validando las preguntas obligatorias.

Se debe tener una sección “Mis Respuestas” para visualizar las respuestas a las encuestas realizadas
por el encuestado
El encuestado puede contestar un máximo de 3 veces una misma encuesta.

Backend
El backend del sistema se debe desarrollar con las siguientes consideraciones:
- Web Api RESTfull con .Net 6 (.net core)
- Lenguaje C#
- Base de datos con PostgreSQL (preferible usar Entity framework)
- Validación de usuarios:
  - Se puede utilizar cualquiera de estas opciones
- Usuario y contraseña en encabezado de petición
- JWT (preferible)
- Documentar API con Swagger
Frontend
El frontend se debe desarrollar con las siguientes consideraciones:
- Angular 16+
- TypeScript
- Librería de UI (Angular Material)
- Protección de rutas