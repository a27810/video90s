Actividad de Aprendizaje 2 - San valero - 2º DAW - Desarrollo Web Entorno Servidor 
----------------------------------------------------------------------------------

# Video90s 🎥

**Video90s** es una API RESTful para un videoclub : usuarios pueden registrarse, iniciar sesión, navegar por catálogos de películas y gestionar sus propios alquileres.

---

## ✅ Funcionalidades obligatorias implementadas

1. **Arquitectura RESTful + CRUD + códigos HTTP**  
   - Gestión de usuarios, películas y alquileres.  
   - Zona pública: listar y buscar películas.  
   - Zona privada: ver/alquilar películas, gestionar usuarios y alquileres.

2. **Modelo de datos con EF Core (Code-First)**  
   - Tres entidades: `User`, `Movie`, `Rental`.  
   - Relaciones: un usuario puede tener múltiples alquileres; cada alquiler enlaza a una película.  
   - Cada entidad tiene ≥6 propiedades (strings, int, decimal, bool, DateTime).

3. **Búsqueda, filtrado y ordenación**  
   - Filtros en público y privado por **género** y **disponibilidad**.  
   - Ordenación asc/desc por **precio** y **fecha** (no se usa `Id`).

4. **Autenticación y autorización JWT**  
   - Roles **Admin** y **User**.  
   - Endpoints protegidos con `[Authorize]`.

5. **Contenerización con Docker Compose**  
   - **API** en puerto **7810**  
   - **SQL Server** en puerto **2781**  
   - Orquestación de contenedores en `docker-compose.yml`

---

## 🚀 Funcionalidades extra ya realizadas

1. **Git + GitFlow**  
2. **Colección Postman** (`video90s-postman-collection.json`)  
3. **EF Core Migrations** (Code-First, migraciones automáticas)  
4. **Cliente web contenerizado** (API + base de datos + front opcional)  
5. **Cliente consola contenerizado** (`video90s-client`)  
6. **Integración con API externa** (TVMaze via `HttpClient`)  

---

¡Listo para clonar, arrancar con `docker-compose up` y probar!  
