using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace GESTION4
{
    public partial class Form1 : Form
    {
        private readonly string _connString;

        public Form1()
        {
            InitializeComponent();
            _connString = ConfigurationManager.ConnectionStrings["cnClientes"].ConnectionString;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarProveedores();
        }

        private void btnGuardar1_Click(object sender, EventArgs e)
        {
            using (var cn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand("sp_InsertarProveedor", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text.Trim());
                cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@Ciudad", txtCiudad.Text.Trim());

                cn.Open();
                var nuevoID = cmd.ExecuteScalar();
                MessageBox.Show($"Proveedor insertado con ID = {nuevoID}", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LimpiarCampos();
            CargarProveedores();
        }

        private void CargarProveedores()
        {
            using (var cn = new SqlConnection(_connString))
            using (var da = new SqlDataAdapter("SELECT * FROM Proveedor", cn))
            {
                var dt = new DataTable();
                da.Fill(dt);
                dgvProveedores.DataSource = dt;
                dgvProveedores.AutoResizeColumns();
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtCiudad.Clear();
            txtNombre.Focus();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
