# Clínica Dental - Gestión de Citas

## Descripción
Aplicación web para gestión de citas desarrollada con ASP.NET Core MVC, permitiendo realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) sobre citas médicas.

## Funcionalidades del CRUD

### Crear Cita
- Registro de nuevas citas
- Validación de datos del paciente
- Cálculo automático de precios según procedimiento
- Verificación de disponibilidad de fecha y hora
  
### Notificación por Correo Electrónico
✉️ Características del sistema de correos:
- Envío automático de confirmación al crear una cita
- Detalles incluidos en el correo:
  * Nombre del paciente
  * Fecha y hora de la cita
  * Procedimiento
  * Detalles de precio
- Uso de servicio de envío de correos integrado
- Manejo de errores en el envío de correos

### Listar Citas
- Visualización de todas las citas
- Interfaz moderna y responsive
- Detalles básicos de cada cita

### Editar Cita
- Modificación de información de cita existente
- Validación de datos actualizados
- Recálculo de precios al cambiar procedimiento
- Control de disponibilidad de fecha/hora

### Eliminar Cita
- Eliminación de citas
- Confirmación antes de eliminar
- Manejo de errores

## Tecnologías Utilizadas
- ASP.NET Core MVC
- Entity Framework Core
- Bootstrap 5
- jQuery
- SQL Server

## Características Técnicas
- Validación del lado del cliente y servidor
- Manejo de excepciones
- Patrón Repository
- Responsive Design

## Requisitos
- .NET 6.0 o superior
- SQL Server
- Visual Studio 2022

## Instalación
1. Clonar repositorio
2. Restaurar paquetes NuGet
3. Configurar cadena de conexión
4. Aplicar migraciones
5. Ejecutar proyecto
