using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace OOP25
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void проПрограмуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout form2 = new FormAbout();
            form2.Show();
        }

        private void button1_Click(object sender, EventArgs e) // Завантажити таблицю
        {
            // Очищуємо dataGridView1
            dataGridView1.Rows.Clear();

            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=Football.mdb";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            OleDbDataReader dbReader = null;
            try
            {
                dbConnection.Open();
                string query = "SELECT * FROM Football";
                OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);
                dbReader = dbCommand.ExecuteReader();

                // Перевіряємо дані
                if (dbReader.HasRows == false)
                {
                    MessageBox.Show("Дані не знайдено!", "Помилка!");
                }
                else
                {
                    // Зчитуємо дані
                    while (dbReader.Read())
                    {
                        // Виводимо дані
                        dataGridView1.Rows.Add(dbReader["id"], dbReader["team"], dbReader["name"], dbReader["surname"], dbReader["playernumber"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                MessageBox.Show($"Повідомлення: {ex.Message}", "Викликано виняток!");
            }
            finally
            {
                // Закриваємо з'єднання з БД
                dbReader.Close();
                dbConnection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e) // Додати рядок до таблиці
        {
            // Перевіряємо, що хоча б один рядок вибраний
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Виберіть хоча б один рядок!", "Увага!");
                return;
            }

            // Створюємо з'єднання
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=Football.mdb";  // рядок з'єднання
            OleDbConnection dbConnection = new OleDbConnection(connectionString);

            try
            {
                // Відкриваємо з'єднання
                dbConnection.Open();

                // Для кожного вибраного рядка
                foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
                {
                    // Перевіряємо дані в рядку
                    if (selectedRow.Cells[0].Value == null ||
                        selectedRow.Cells[1].Value == null ||
                        selectedRow.Cells[2].Value == null ||
                        selectedRow.Cells[3].Value == null ||
                        selectedRow.Cells[4].Value == null)
                    {
                        MessageBox.Show("Не всі дані введені для рядка " + selectedRow.Index + "!", "Увага!");
                        continue;
                    }

                    // Зчитуємо дані
                    string id = selectedRow.Cells[0].Value.ToString();
                    string team = selectedRow.Cells[1].Value.ToString();
                    string name = selectedRow.Cells[2].Value.ToString();
                    string surname = selectedRow.Cells[3].Value.ToString();
                    string playernumber = selectedRow.Cells[4].Value.ToString();

                    // Виконуємо запит до БД
                    string query = "INSERT INTO Football VALUES (" + id + ", '" + team + "', '" + name + "', '" + surname + "', " + playernumber + ")";
                    OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);

                    // Виконуємо запит
                    if (dbCommand.ExecuteNonQuery() != 1)
                        MessageBox.Show("Помилка виконання запиту для рядка " + selectedRow.Index + "!", "Помилка!");
                    else
                        MessageBox.Show("Дані додано для рядка " + selectedRow.Index + "!", "Увага!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Повідомлення: {ex.Message}", "Викликано виняток!");
            }
            finally
            {
                // Закриваємо з'єднання з БД
                dbConnection.Close();
            }

        }

        private void button4_Click(object sender, EventArgs e) // Оновити таблицю
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Виберіть хоча б один рядок!", "Увага!");
                return;
            }

            // Створюємо з'єднання
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=Football.mdb";  // рядок з'єднання
            OleDbConnection dbConnection = new OleDbConnection(connectionString);

            try
            {
                // Відкриваємо з'єднання
                dbConnection.Open();

                // Для кожного вибраного рядка
                foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
                {
                    // Перевіряємо дані в рядку
                    if (selectedRow.Cells[0].Value == null ||
                        selectedRow.Cells[1].Value == null ||
                        selectedRow.Cells[2].Value == null ||
                        selectedRow.Cells[3].Value == null ||
                        selectedRow.Cells[4].Value == null)
                    {
                        MessageBox.Show("Не всі дані введені для рядка " + selectedRow.Index + "!", "Увага!");
                        continue;
                    }

                    // Зчитуємо дані
                    string id = selectedRow.Cells[0].Value.ToString();
                    string team = selectedRow.Cells[1].Value.ToString();
                    string name = selectedRow.Cells[2].Value.ToString();
                    string surname = selectedRow.Cells[3].Value.ToString();
                    string playernumber = selectedRow.Cells[4].Value.ToString();

                    // Виконуємо запит до БД
                    string query = "UPDATE Football SET team = '" + team + "', name = '" + name + "', surname = '" + surname + "', playernumber = " + playernumber + " WHERE id = " + id;
                    OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);

                    // Виконуємо запит
                    if (dbCommand.ExecuteNonQuery() != 1)
                        MessageBox.Show("Помилка виконання запиту для рядка " + selectedRow.Index + "!", "Помилка!");
                    else
                        MessageBox.Show("Дані оновлено для рядка " + selectedRow.Index + "!", "Увага!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Повідомлення: {ex.Message}", "Викликано виняток!");
            }
            finally
            {
                // Закриваємо з'єднання з БД
                dbConnection.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e) // Видалити рядок з таблиці
        {
            // Перевіряємо, що хоча б один рядок вибрано
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Виберіть хоча б один рядок!", "Увага!");
                return;
            }
            DialogResult result = MessageBox.Show("Ви впевнені, що бажаєте видалити вибрані рядки?", "Підтвердження видалення", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // Створюємо список для зберігання ідентифікаторів вибраних рядків
                List<string> selectedIds = new List<string>();

                // Отримуємо ідентифікатори вибраних рядків і додаємо їх до списку
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        string id = row.Cells[0].Value.ToString();
                        selectedIds.Add(id);
                    }
                }

                // Перевіряємо, що хоча б один ідентифікатор був отриманий
                if (selectedIds.Count == 0)
                {
                    MessageBox.Show("Не всі дані введені!", "Увага!");
                    return;
                }

                // Створюємо з'єднання
                string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=Football.mdb";
                OleDbConnection dbConnection = new OleDbConnection(connectionString);

                try
                {
                    // Відкриваємо з'єднання
                    dbConnection.Open();

                    // Формуємо рядок запиту з використанням ідентифікаторів вибраних рядків
                    string query = "DELETE FROM Football WHERE id IN (" + string.Join(",", selectedIds) + ")";
                    OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);

                    // Виконуємо запит
                    int rowsAffected = dbCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Видалено " + rowsAffected + " рядків!", "Увага!");
                    }
                    else
                    {
                        MessageBox.Show("Немає вибраних рядків для видалення!", "Увага!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Повідомлення: {ex.Message}", "Викликано виняток!");
                }
                finally
                {
                    // Закриваємо з'єднання з БД
                    dbConnection.Close();
                }
            }
        }
    }
}
