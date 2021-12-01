using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace TrabalhoPedrin
{
    public partial class Form1 : Form
    {
        MySqlConnection Conexao;
        private string data_source = "server=127.0.0.1;uid=root;pwd=;database=db_petshop";

        private int ?id_contato_selecionado = null;

        public Form1()
        {
            InitializeComponent();

            lstClientes.View = View.Details;
            lstClientes.LabelEdit = true;
            lstClientes.AllowColumnReorder = true;
            lstClientes.FullRowSelect = true;
            lstClientes.GridLines = true;

            lstClientes.Columns.Add("ID", 30, HorizontalAlignment.Left);
            lstClientes.Columns.Add("Nome", 150, HorizontalAlignment.Left);
            lstClientes.Columns.Add("E-mail", 150, HorizontalAlignment.Left);
            lstClientes.Columns.Add("Endereço", 150, HorizontalAlignment.Left);
            lstClientes.Columns.Add("Complemento", 100, HorizontalAlignment.Left);
            lstClientes.Columns.Add("Telefone", 100, HorizontalAlignment.Left);

            Carregar_contatos();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // conexao com MySQL
                Conexao = new MySqlConnection(data_source);

                Conexao.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conexao;

                if(id_contato_selecionado == null)
                {
                    cmd.CommandText = "INSERT INTO cliente (nome, email, endereco, complemento, telefone) " +
                                  "VALUES (@nome, @email, @endereco, @complemento, @telefone)";

                    cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@endereco", txtEnd.Text);
                    cmd.Parameters.AddWithValue("@complemento", txtComp.Text);
                    cmd.Parameters.AddWithValue("@telefone", txtTel.Text);
                    cmd.Prepare();

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Cliente Inserido com Sucesso!",
                                    "Sucesso!", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
                else
                {
                    cmd.CommandText = "UPDATE cliente SET nome=@nome, email=@email, endereco=@endereco, complemento=@complemento, telefone=@telefone " +
                                      "WHERE id=@id ";

                    cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@endereco", txtEnd.Text);
                    cmd.Parameters.AddWithValue("@complemento", txtComp.Text);
                    cmd.Parameters.AddWithValue("@telefone", txtTel.Text);
                    cmd.Parameters.AddWithValue("@id", id_contato_selecionado);

                    cmd.Prepare();

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Cliente Atualizado com Sucesso!",
                                    "Sucesso!", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }

                Zerar_form();

                Carregar_contatos();

            } catch(MySqlException ex)
            {
                MessageBox.Show("Erro " + ex.Number + " ocorreu: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } catch (Exception ex)
            {
                MessageBox.Show("Erro ocorreu: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();
            }

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtComp_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                Conexao = new MySqlConnection(data_source);
                Conexao.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = Conexao;
                cmd.CommandText = "SELECT * FROM cliente WHERE nome LIKE @q OR email LIKE @q ";
                cmd.Parameters.AddWithValue("@q", "%" + txtBuscar.Text + "%");
                cmd.Prepare();

                MySqlDataReader reader = cmd.ExecuteReader();

                lstClientes.Items.Clear();

                while (reader.Read())
                {
                    string[] row =
                    {
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetString(5),
                    };

                    lstClientes.Items.Add(new ListViewItem(row));
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro " + ex.Number + " ocorreu: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ocorreu: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();
            }
        }
        private void Carregar_contatos()
        {
            try
            {
                Conexao = new MySqlConnection(data_source);
                Conexao.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = Conexao;
                cmd.CommandText = "SELECT * FROM cliente ORDER BY id DESC ";
                cmd.Prepare();

                MySqlDataReader reader = cmd.ExecuteReader();

                lstClientes.Items.Clear();

                while (reader.Read())
                {
                    string[] row =
                    {
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetString(5),
                    };

                    lstClientes.Items.Add(new ListViewItem(row));
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro " + ex.Number + " ocorreu: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ocorreu: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();
            }
        }

        private void lstClientes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstClientes_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ListView.SelectedListViewItemCollection itensSelecionados = lstClientes.SelectedItems;

            foreach (ListViewItem item in itensSelecionados)
            {
                id_contato_selecionado = Convert.ToInt32(item.SubItems[0].Text);

                txtNome.Text = item.SubItems[1].Text;
                txtEmail.Text = item.SubItems[2].Text;
                txtTel.Text = item.SubItems[3].Text;
                txtComp.Text = item.SubItems[4].Text;
                txtEnd.Text = item.SubItems[5].Text;

                button4.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Zerar_form();
        }

        private void Zerar_form()
        {
            id_contato_selecionado = null;

            txtNome.Text = String.Empty;
            txtEmail.Text = "";
            txtEnd.Text = "";
            txtComp.Text = "";
            txtTel.Text = "";
            txtNome.Focus();
            button4.Enabled = false;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Excluir_contato();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Excluir_contato();
        }

        private void Excluir_contato()
        {
            try
            {
                DialogResult conf = MessageBox.Show("Deseja deletar este cliente?",
                                "Confirmar exclusão",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning);
                if (conf == DialogResult.Yes)
                {
                    Conexao = new MySqlConnection(data_source);
                    Conexao.Open();

                    MySqlCommand cmd = new MySqlCommand();

                    cmd.Connection = Conexao;

                    cmd.CommandText = "DELETE FROM cliente WHERE id=@id ";

                    cmd.Parameters.AddWithValue("@id", id_contato_selecionado);

                    cmd.Prepare();

                    cmd.ExecuteNonQuery();


                    MessageBox.Show("Cliente Deletado com Sucesso!",
                                    "Sucesso!", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    Carregar_contatos();

                    Zerar_form();
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro " + ex.Number + " ocorreu: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ocorreu: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();
            }
        }
    }
}
