using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;

namespace AppWebClinica.Models
{
    public class Email
    {

        public void Enviar(Cita cita)
        {
            try
            {
                // Crear el objeto email
                MailMessage email = new MailMessage
                {
                    Subject = "Datos de la cita en plataforma web clinica CR",
                    From = new MailAddress("jabs0696@gmail.com"), // Cambia por tu correo
                    IsBodyHtml = true,
                    Priority = MailPriority.Normal
                };

                // Agregar destinatarios
                email.To.Add(new MailAddress(cita.Email));

                // Cuerpo del correo en HTML
                string html = @"
<div style='font-family: ""Helvetica Neue"", Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 30px; background-color: #ffffff; border-radius: 12px; box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);'>
    <!-- Encabezado con logo -->
    <div style='text-align: center; padding-bottom: 25px;'>
        <img src='https://drive.google.com/uc?export=view&id=1hjxS6Z8UnBzMdWxaO5HlFN1B86u8INWo' alt='Logo Clínica Dental' style='margin-bottom: 15px; max-width: 100%; height: auto;'>
        <h1 style='color: #2E7D32; margin: 0; font-size: 28px; font-weight: 600;'>¡Bienvenido, " + cita.Nombre + @"!</h1>
        <p style='color: #666; margin: 8px 0; font-size: 16px;'>Clínica Dental del Pacífico</p>
        <div style='width: 50px; height: 3px; background: #4CAF50; margin: 20px auto;'></div>
    </div>

    <!-- Mensaje de confirmación -->
    <div style='padding: 25px; background-color: #f8f9fa; border-radius: 10px; margin-bottom: 25px;'>
        <p style='color: #444; font-size: 16px; line-height: 1.6; margin-top: 0;'>
            Nos complace confirmar tu cita dental. Hemos reservado tu espacio y estamos listos para brindarte la mejor atención.
        </p>
    </div>

    <!-- Detalles de la cita -->
    <div style='background-color: #ffffff; border: 1px solid #e0e0e0; border-radius: 10px; padding: 25px; margin-bottom: 25px;'>
        <h2 style='color: #2E7D32; margin: 0 0 20px 0; font-size: 20px;'>Detalles de tu Cita</h2>
        
        <table style='width: 100%; border-collapse: collapse;'>
            <tr>
                <td style='padding: 12px 0; border-bottom: 1px solid #f0f0f0;'>
                    <strong style='color: #555;'>Cédula:</strong>
                </td>
                <td style='padding: 12px 0; border-bottom: 1px solid #f0f0f0; color: #666;'>
                    " + cita.Cedula + @"
                </td>
            </tr>
            <tr>
                <td style='padding: 12px 0; border-bottom: 1px solid #f0f0f0;'>
                    <strong style='color: #555;'>Email:</strong>
                </td>
                <td style='padding: 12px 0; border-bottom: 1px solid #f0f0f0; color: #666;'>
                    " + cita.Email + @"
                </td>
            </tr>
            <tr>
                <td style='padding: 12px 0; border-bottom: 1px solid #f0f0f0;'>
                    <strong style='color: #555;'>Fecha y hora:</strong>
                </td>
                <td style='padding: 12px 0; border-bottom: 1px solid #f0f0f0; color: #666;'>
                    " + cita.FechaHora + @"
                </td>
            </tr>
            <tr>
                <td style='padding: 12px 0; border-bottom: 1px solid #f0f0f0;'>
                    <strong style='color: #555;'>Procedimiento:</strong>
                </td>
                <td style='padding: 12px 0; border-bottom: 1px solid #f0f0f0; color: #666;'>
                    " + cita.Procedimiento + @"
                </td>
            </tr>
        </table>
    </div>

    <!-- Detalles del pago -->
    <div style='background-color: #f8f9fa; border-radius: 10px; padding: 25px; margin-bottom: 25px;'>
        <h2 style='color: #2E7D32; margin: 0 0 20px 0; font-size: 20px;'>Detalles del Pago</h2>
        
        <table style='width: 100%; border-collapse: collapse;'>
            <tr>
                <td style='padding: 12px 0; border-bottom: 1px solid #e0e0e0;'>
                    <strong style='color: #555;'>Precio base:</strong>
                </td>
                <td style='padding: 12px 0; border-bottom: 1px solid #e0e0e0; color: #666; text-align: right;'>
                    ₡" + cita.Precio + @"
                </td>
            </tr>
            <tr>
                <td style='padding: 12px 0; border-bottom: 1px solid #e0e0e0;'>
                    <strong style='color: #555;'>Impuesto:</strong>
                </td>
                <td style='padding: 12px 0; border-bottom: 1px solid #e0e0e0; color: #666; text-align: right;'>
                    ₡" + cita.Impuesto + @"
                </td>
            </tr>
            <tr>
                <td style='padding: 12px 0; border-bottom: 1px solid #e0e0e0;'>
                    <strong style='color: #555;'>Adelanto realizado:</strong>
                </td>
                <td style='padding: 12px 0; border-bottom: 1px solid #e0e0e0; color: #666; text-align: right;'>
                    ₡" + cita.Adelanto + @"
                </td>
            </tr>
            <tr>
                <td style='padding: 12px 0;'>
                    <strong style='color: #2E7D32; font-size: 18px;'>Monto total:</strong>
                </td>
                <td style='padding: 12px 0; color: #2E7D32; font-size: 18px; font-weight: bold; text-align: right;'>
                    ₡" + cita.Total + @"
                </td>
            </tr>
        </table>
    </div>

    <!-- Recordatorios importantes -->
    <div style='background-color: #FFF3E0; border-radius: 10px; padding: 20px; margin-bottom: 25px;'>
        <h3 style='color: #E65100; margin: 0 0 15px 0; font-size: 18px;'>Recordatorios Importantes</h3>
        <ul style='color: #666; margin: 0; padding-left: 20px; line-height: 1.6;'>
            <li>Por favor, llega 10 minutos antes de tu cita.</li>
            <li>Si necesitas cancelar o reprogramar, házlo con al menos 24 horas de anticipación.</li>
            <li>Trae tu cédula de identidad y comprobante de pago.</li>
        </ul>
    </div>

    <!-- Información de contacto -->
    <div style='text-align: center; border-top: 1px solid #e0e0e0; padding-top: 25px;'>
        <p style='color: #666; margin-bottom: 5px;'>¿Tienes preguntas? Contáctanos:</p>
        <p style='color: #2E7D32; font-weight: bold; margin: 5px 0;'>+506 2222-2222</p>
        <p style='color: #2E7D32; font-weight: bold; margin: 5px 0;'>info@clinicadentalpacífico.com</p>
    </div>

    <!-- Pie de página -->
    <div style='text-align: center; margin-top: 30px;'>
        <p style='color: #999; font-size: 12px; margin: 5px 0;'>Este es un correo generado automáticamente, por favor no responder.</p>
        <p style='color: #999; font-size: 12px; margin: 5px 0;'>© 2025 Clínica Dental del Pacífico. Todos los derechos reservados.</p>
    </div>
</div>";

                email.Body = html;

                // Configurar SMTP para Gmail
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("jabs0696@gmail.com", "fobukqqitntbcfbi"), // Cambia por tus credenciales
                    EnableSsl = true
                };

                // Enviar el correo
                smtp.Send(email);

                // Liberar recursos
                email.Dispose();
                smtp.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al enviar el correo electrónico: " + ex.Message, ex);
            }
        }
    }
}