//Josue Barboza Segura B80937
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AppWebClinica.Models

{
    public class Cita
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar la cédula")]
        [DataType(DataType.Text)]
        public int Cedula { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre completo")]
        [StringLength(100)]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar el email")]
        //[EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido")]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe seleccionar la fecha")]
        [DataType(DataType.DateTime)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Remote("CheckAvailability", "Citas", ErrorMessage = "Ya existe una cita en la fecha y hora seleccionada.")]
        public DateTime FechaHora { get; set; }



        [Required(ErrorMessage = "Debe seleccionar un procedimiento")]
        public string Procedimiento { get; set; }

        public decimal Precio { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public decimal Adelanto { get; set; }

    }//Fin de la clase cita
}//Fin de namespace
