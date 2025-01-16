//Josue Barboza Segura B80937
using Microsoft.AspNetCore.Mvc;
//Se referencia al ORM
using AppWebClinica.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
namespace AppWebClinica.Controllers
{
    public class CitasController : Controller
    {
        //Variable que permite manejar la referencia del contexto
        private readonly DbContextClinica _context = null;

        /// <param name="context"></param
        public CitasController(DbContextClinica context)
        {
            _context = context;// se asigna la referencia ael contexto
        }

        public async Task<IActionResult> Index()
        {
            var listadoPacientes = await _context.Citas.ToListAsync();

            return View(listadoPacientes);

        }

        //Método encargado para almacenar una cita
        [HttpGet]
        public IActionResult Create() //Método encargado de mostrar el front-end para crear una cita
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind] Cita cita)
        {
            // Validación de la cédula
            if (!EsCedulaValida(cita.Cedula.ToString()))
            {
                ModelState.AddModelError("Cedula", "La cédula ingresada no es válida.");
            }

            // Validación del correo electrónico usando el atributo [EmailAddress]
            if (!new EmailAddressAttribute().IsValid(cita.Email))
            {
                ModelState.AddModelError("Email", "El correo electrónico ingresado no es válido.");
            }

            if (ModelState.IsValid) // Verifica si el modelo es válido después de las validaciones
            {
                cita.Id = 0; // Se asigna 0 para generar el id automáticamente
                _context.Citas.Add(cita); // Si hay datos, se almacena la cita
                await _context.SaveChangesAsync(); // Se aplican los cambios en la db
                Email email = new Email();
                email.Enviar(cita);
                return RedirectToAction("Index"); // Ubica al usuario dentro del listado de citas
            }
            else
            {
                return View(cita); // Si hay errores de validación, se devuelve a la vista con los mensajes de error
            }
        }

        //Procesos para los metodos de eliminacion

        [HttpGet]

        public async Task<IActionResult> Delete(int id)
        {
            //Buscar la cita a eliminar
            var temp = await _context.Citas.FirstOrDefaultAsync(r => r.Id == id);

            //Se envian los datos de la cita al front-end
            return View(temp);
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int? id)
        {
            //se busca por el id la cita a eliminar
            var temp = await _context.Citas.FirstOrDefaultAsync(r => r.Id == id);

            if (temp != null)//Se valida si tiene datos
            {
                _context.Citas.Remove(temp);//Se elimina la cita
                await _context.SaveChangesAsync();//se aplican los cambios
               

                return RedirectToAction("Index");//se muestra la lista de citas
            }
            else
            {
                return NotFound();//Error 404 recurso no disponible
            }
        }

        //Metodo encargado de mostrar los datos para una cita especifica

        [HttpGet]

        public async Task<IActionResult> Details(int id)
        {
            //Se busca por el id la cita a mostrar
            var temp = await _context.Citas.FirstOrDefaultAsync(r => r.Id == id);

            //Se envian los datos al front-end
            return View(temp);
        }


        //Metodos encargados de realizar el proceso de editar los datos de una cita

        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            //Se busca por el id los datos a Editar
            var temp = await _context.Citas.FirstOrDefaultAsync(r => r.Id == id);

            //Se envia los datos de la cita al front-end
            return View(temp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind] Cita pcita)
        {
            //Validacion del ID
            if (id == pcita.Id)
            {
                //Se busca la cita anterior con sus datos
                var temp = await _context.Citas.FirstOrDefaultAsync(r => r.Id == id);

                _context.Citas.Remove(temp);//Se elimina la cita
                _context.Citas.Add(pcita);//Se agrega la cita
                await _context.SaveChangesAsync();//Se aplican los cambios

                //Se ubica al usuario dentro del listado de citas
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();//Recurso no disponible
            }
        }

        // Este método se utiliza para verificar la disponibilidad de una cita en una fecha y hora específicas
        public async Task<IActionResult> CheckAvailability(DateTime fechaHora, int? currentId = null)
        {
            // Busca citas en la misma fecha y hora, excluyendo la cita actual en edición
            var cita = await _context.Citas
                .FirstOrDefaultAsync(c => c.FechaHora == fechaHora && c.Id != currentId);

            if (cita != null)
            {
                return Json("Ya existe una cita en la fecha y hora seleccionada.");
            }
            else
            {
                return Json(true);
            }
        }

        public bool EsCedulaValida(string cedula)
        {
            // Ejemplo de validación para una cédula con 9 dígitos numéricos
            var patronCedula = @"^\d{9}$";
            return Regex.IsMatch(cedula, patronCedula);
        }



    }//Fin de la clase 
}//Fin del namespace
