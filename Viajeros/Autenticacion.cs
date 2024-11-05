using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Viajeros
{
    public class Autenticacion : IHospedaje
    {
        // Cliente HTTP para realizar solicitudes
        private readonly HttpClient httpClient;

        // Constructor que inicializa el cliente HTTP
        public Autenticacion()
        {
            // Crea una instancia de HttpClient
            httpClient = new HttpClient();
        }

        //Método para realizar el login
        public async Task<string> Login(string username, string password)
        {
            //Realiza una solicitud para autenticar al usuario
            var response = await httpClient.PostAsJsonAsync("", new { username, password });

            // Asegura que la respuesta sea exitosa;
            response.EnsureSuccessStatusCode();

            // Lee el contenido de la respuesta como un string, que contendrá el token
            var token = await response.Content.ReadAsStringAsync();

            // Agrega el token al encabezado de autorización del cliente para futuras solicitudes
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Retorna el token obtenido
            return token;
        }

        // Método para enviar el parte de viajeros
        public async Task<bool> EnviarParteViajeros(ParteViajeros parte)
        {
            // Serializa el objeto parte en JSON
            var json = JsonSerializer.Serialize(parte);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Realiza una solicitud POST para enviar el parte de viajeros
            var response = await httpClient.PostAsync("", content);

            // Verifica si la solicitud fue exitosa
            return response.IsSuccessStatusCode;
        }

        // Clase que representa el parte de viajeros
        public class ParteViajeros
        {
            public string HotelNombre { get; set; }
            public string HotelDireccion { get; set; }
            public List<Viajero> Viajeros { get; set; }
        }

        // Clase que representa a un viajero
        public class Viajero
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string DNI { get; set; }
            public string Direccion { get; set; }
            public string Provincia { get; set; }
            public string Email { get; set; }
            public DateTime FechaLlegada { get; set; }
            public DateTime FechaSalida { get; set; }
        }
    }
}

