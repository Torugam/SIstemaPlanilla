using SistemaPlanilla;
using SistemaPlanilla.Modelo;
using SIstemaPlanilla;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaPlanilla.Vista
{
    public partial class Login : Form
    {
        private Panel panelLateral = null!;
        private Label lblTituloSistema = null!;
        private Label lblSubtitulo = null!;
        private Label lblTituloLogin = null!;
        private Label lblUsuario = null!;
        private TextBox txtUsuario = null!;
        private Label lblPassword = null!;
        private TextBox txtPassword = null!;
        private Button btnIngresar = null!;
        private Label lblError = null!;

        public Login()
        {
            ConstruirDiseñoFormulario();
        }

        private void ConstruirDiseñoFormulario()
        {
            this.Size = new Size(750, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.White;
            this.Text = "Inicio de Sesión - Sistema de Planillas";

            panelLateral = new Panel
            {
                Size = new Size(280, 450),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(24, 43, 73)
            };

            lblTituloSistema = new Label
            {
                Text = "SISTEMA DE\nPLANILLAS",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Location = new Point(25, 140),
                AutoSize = true
            };

            lblSubtitulo = new Label
            {
                Text = "Gestión de Recursos Humanos\ny Control de Nóminas",
                ForeColor = Color.FromArgb(170, 190, 215),
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Location = new Point(27, 220),
                AutoSize = true
            };

            panelLateral.Controls.Add(lblTituloSistema);
            panelLateral.Controls.Add(lblSubtitulo);

            lblTituloLogin = new Label
            {
                Text = "Iniciar Sesión",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 30, 30),
                Location = new Point(330, 45),
                AutoSize = true
            };

            lblUsuario = new Label
            {
                Text = "Usuario:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 60),
                Location = new Point(330, 105),
                AutoSize = true
            };

            txtUsuario = new TextBox
            {
                Font = new Font("Segoe UI", 11),
                Location = new Point(330, 130),
                Size = new Size(350, 30)
            };

            lblPassword = new Label
            {
                Text = "Contraseña:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 60),
                Location = new Point(330, 185),
                AutoSize = true
            };

            txtPassword = new TextBox
            {
                Font = new Font("Segoe UI", 11),
                Location = new Point(330, 210),
                Size = new Size(350, 30),
                PasswordChar = '*'
            };

            lblError = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.DarkRed,
                Location = new Point(330, 250),
                Size = new Size(350, 35),
                Visible = false
            };

            btnIngresar = new Button
            {
                Text = "INGRESAR AL SISTEMA",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(13, 110, 253),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(330, 295),
                Size = new Size(350, 42),
                Cursor = Cursors.Hand
            };
            btnIngresar.FlatAppearance.BorderSize = 0;
            btnIngresar.Click += new EventHandler(btnIngresar_Click);

            this.Controls.Add(panelLateral);
            this.Controls.Add(lblTituloLogin);
            this.Controls.Add(lblUsuario);
            this.Controls.Add(txtUsuario);
            this.Controls.Add(lblPassword);
            this.Controls.Add(txtPassword);
            this.Controls.Add(lblError);
            this.Controls.Add(btnIngresar);
        }

        private void btnIngresar_Click(object? sender, EventArgs e)
        {
            string username = txtUsuario.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MostrarError("Por favor ingrese su usuario y contraseña.");
                return;
            }

            string passwordHash = HelperSeguridad.CalcularSHA256(password);

            using (SqlConnection conn = Conexion.GetConexion())
            {
                if (conn == null)
                {
                    MostrarError("Fallo al conectar con la base de datos. Verifique su red o conexión SQL.");
                    return;
                }

                using (SqlCommand cmd = new SqlCommand("SP_ValidarLogin", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p_username", username);
                    cmd.Parameters.AddWithValue("@p_password_hash", passwordHash);

                    cmd.Parameters.Add("@p_id_usuario", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@p_id_rol", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@p_nombre_rol", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@p_nombre_empleado", SqlDbType.VarChar, 150).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@p_codigo_respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@p_mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                    try
                    {
                        cmd.ExecuteNonQuery();

                        int codigoRespuesta = Convert.ToInt32(cmd.Parameters["@p_codigo_respuesta"].Value);
                        string mensaje = cmd.Parameters["@p_mensaje"].Value?.ToString() ?? "";

                        if (codigoRespuesta == 1)
                        {
                            SesionUsuario.IdUsuario = Convert.ToInt32(cmd.Parameters["@p_id_usuario"].Value);
                            SesionUsuario.IdRol = Convert.ToInt32(cmd.Parameters["@p_id_rol"].Value);
                            SesionUsuario.Username = username;
                            SesionUsuario.NombreRol = cmd.Parameters["@p_nombre_rol"].Value?.ToString() ?? "";
                            SesionUsuario.NombreEmpleado = cmd.Parameters["@p_nombre_empleado"].Value?.ToString() ?? "";

                            MessageBox.Show($"¡Bienvenido {SesionUsuario.NombreEmpleado}!", "Acceso Concedido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            Form1 frmPrincipal = new Form1();
                            this.Hide();
                            frmPrincipal.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MostrarError(mensaje);
                        }
                    }
                    catch (Exception ex)
                    {
                        MostrarError("Error de validación: " + ex.Message);
                    }
                }
            }
        }

        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }
    }
}