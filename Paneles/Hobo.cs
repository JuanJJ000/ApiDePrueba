using Domain.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paneles
{
    public partial class Hobo : Form
    {
        public Hobo()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            AddPanadero();

        }

        private async void AddPanadero()
        {



            PanaderoViewModel panaderoViewModel = new PanaderoViewModel();
            panaderoViewModel.Nombre = txtNombre.Text;
            panaderoViewModel.Apellido = txtApellido.Text;
            panaderoViewModel.Edad = int.Parse(txtEdad.Text);
            panaderoViewModel.Carnet = txtCarnet.Text;

            using (var client = new HttpClient())
            {
                var serializedPanadero = JsonConvert.SerializeObject(panaderoViewModel);
                var content = new StringContent(serializedPanadero, Encoding.UTF8,
                    "application/json");
                var result = await client.PostAsync("https://localhost:44376/api/Panadero",
                    content);
                if (result.IsSuccessStatusCode)
                    MessageBox.Show("Panadero registrado correctamente");
                else
                    MessageBox.Show("No se logró registrar el Panadero: " + result.Content.ReadAsStringAsync().Result);
            }

            GetAllPanaderoView();
        }


        private async void GetAllPanaderoView()
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync("https://localhost:44376/api/Panadero"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var PanaderoJsonString = await response.Content.ReadAsStringAsync();
                        dtgPanaderia.DataSource = JsonConvert.
                           DeserializeObject<List<PanaderoViewModel>>(PanaderoJsonString)
                            .ToList();
                    }
                    else
                    {
                        MessageBox.Show("No se puede obtener el panadero: " + response.StatusCode);
                    }
                }
            }
        }


        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtIDBusca.Text))
            {
                MessageBox.Show("Por favor ingresar el id del panadero a buscar");
                return;
            }
            UpdatePanadero(int.Parse(txtIDBusca.Text));

        }

        private void Limpia()
        {
            txtApellido.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtCarnet.Text = string.Empty;
            txtEdad.Text = string.Empty;
            txtIDBusca.Text = string.Empty;
        }


        private async void UpdatePanadero(int id)
        {
            PanaderoViewModel oPanadero = new PanaderoViewModel();
            oPanadero.Id = id;
            oPanadero.Nombre = txtNombre.Text;
            oPanadero.Apellido = txtApellido.Text;
            oPanadero.Edad = int.Parse(txtEdad.Text);
            oPanadero.Carnet = txtCarnet.Text;
        

            using (var client = new HttpClient())
            {

                var serializedPanadero = JsonConvert.SerializeObject(oPanadero);
                var content = new StringContent(serializedPanadero, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync("https://localhost:44376/api/Panadero/" + oPanadero.Id, content);
                if (response.IsSuccessStatusCode)
                    MessageBox.Show("Panadero actualizado");
                else
                    MessageBox.Show("Error al actualizar el Panadero: " + response.StatusCode);
            }

            Limpia();
            GetAllPanaderoView();
        }



        private async void DeletePanadero(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44376/api/Panadero");
                HttpResponseMessage result = await client
                    .DeleteAsync(String.Format("{0}/{1}", "https://localhost:44376/api/Panadero", id));
                if (result.IsSuccessStatusCode)
                    MessageBox.Show("Panadero eliminado con éxito");
                else
                    MessageBox.Show("No se pudo eliminar el panadero: " + result.StatusCode);
            }
            Limpia();
            GetAllPanaderoView();
        }



        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDBusca.Text))
            {
                MessageBox.Show("Por favor ingresar el id del panadero a buscar");
                return;
            }

            DeletePanadero(int.Parse( txtIDBusca.Text));
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpia();
        }

        private void Hobo_Load(object sender, EventArgs e)
        {
            GetAllPanaderoView();
        }
    }
}
