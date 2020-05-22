using System;
using System.Windows.Forms;
using EmployeesManagerBL;
using EmployeesManagerBL.Model;

namespace EmployeesManagerUI
{
    public partial class MainForm : Form
    {
        //свойство для конвертирования выбранной строки в объект Employee
        public Employee SelectedEmployee =>
            new Employee
            {
                Id = (int)employeesDGV.SelectedRows[0].Cells[0].Value,
                FirstName = employeesDGV.SelectedRows[0].Cells[1].Value.ToString(),
                LastName = employeesDGV.SelectedRows[0].Cells[2].Value.ToString(),
                Patronymic = employeesDGV.SelectedRows[0].Cells[3].Value.ToString(),
                DateOfBirth = (DateTime)employeesDGV.SelectedRows[0].Cells[4].Value,
                AddressOfResidence = employeesDGV.SelectedRows[0].Cells[5].Value.ToString(),
                Department = employeesDGV.SelectedRows[0].Cells[6].Value.ToString(),
                AboutMe = employeesDGV.SelectedRows[0].Cells[7].Value.ToString()
            };

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            EmployeeForm form = new EmployeeForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                UpdateDataGrid();
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            //если не выбрана ни одна строка - выводим сообщение об ошибке
            if (employeesDGV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Не выбран сотрудник для удаления!");
                return;
            }
            int employeeId = (int)employeesDGV.SelectedRows[0].Cells[0].Value;
            EmployeesHandler.DeleteEmployeeById(employeeId);
            UpdateDataGrid();
        }

        //метод заполняет DataGridView актуальными данными 
        private void UpdateDataGrid()
        {
            employeesDGV.DataSource = EmployeesHandler.GetAllEmployees();
            employeesDGV.Columns[0].Visible = false;
            employeesDGV.Columns[1].HeaderText = "Фамилия";
            employeesDGV.Columns[2].HeaderText = "Имя";
            employeesDGV.Columns[3].HeaderText = "Отчество";
            employeesDGV.Columns[4].HeaderText = "Дата рождения";
            employeesDGV.Columns[5].HeaderText = "Адрес проживания";
            employeesDGV.Columns[6].HeaderText = "Отдел";
            employeesDGV.Columns[7].HeaderText = "О себе";
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            //если не выбрана ни одна строка - выводим сообщение об ошибке
            if (employeesDGV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Не выбран сотрудник для изменения!");
                return;
            }

            EmployeeForm form = new EmployeeForm(SelectedEmployee);
            if (form.ShowDialog() == DialogResult.OK)
            {
                UpdateDataGrid();
            }
        }
        
        private void SearchTB_TextChanged(object sender, EventArgs e)
        {
            employeesDGV.CurrentCell = null;
            //проходим по каждой строке и меняем её видимость в зависимости найден искомый текст в строке или нет
            foreach (DataGridViewRow row in employeesDGV.Rows)
            {
                employeesDGV.Rows[row.Index].Visible = HasTextInRow(row);
            }
        }

        //метод определяет имеется ли в выбранной строке DataGridView искомый текст
        private bool HasTextInRow(DataGridViewRow row)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                //если хотя бы в одной клетке найдено совпадение - возвращаем true
                if (cell.Value.ToString().ToUpper().Contains(searchTB.Text.ToUpper()))
                {
                    return true;
                }
            }

            return false;
        } 

        private void EmployeesDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EmployeeForm form = new EmployeeForm(SelectedEmployee, true);
            if (form.ShowDialog() == DialogResult.OK)
            {
                UpdateDataGrid();
            }
        }
    }
}
