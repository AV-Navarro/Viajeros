using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;


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
            httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
        }

        //Método para realizar el login
        /*public async Task<string> Login(string username, string password)
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
        }*/
        // Método para actualizar una publicación
        public async Task<bool> ActualizarPublicacion(int id, Post publicacionActualizada)
        {
            var json = JsonSerializer.Serialize(publicacionActualizada);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"posts/{id}", content);
            return response.IsSuccessStatusCode;
        }



        public async Task<List<Post>> ObtenerPublicaciones()
        {
            var response = await httpClient.GetAsync("posts");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine(json);

            // Asegúrate de que aquí estás deserializando correctamente
            var publicaciones = JsonSerializer.Deserialize<List<Post>>(json);
            return publicaciones;
        }

        public async Task<bool> CrearPublicacion(Post nuevaPublicacion)
        {
            var json = JsonSerializer.Serialize(nuevaPublicacion);
            var content = new StringContent(json, Encoding.UTF8, "aplication/json");
            var response = await httpClient.PostAsync("posts", content);
            return response.IsSuccessStatusCode;
        }

        // Método para obtener comentarios
        public async Task<List<Comment>> ObtenerComentarios()
        {
            var response = await httpClient.GetAsync("comments");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Comment>>(json);
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

        public class Post
        {
            public int UserId { get; set; }
            public int Id { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }
        }

        public class Comment
        {
            public int PostId { get; set; }
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Body { get; set; }
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

