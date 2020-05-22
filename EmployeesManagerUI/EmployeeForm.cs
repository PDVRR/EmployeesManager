using System;
using System.Windows.Forms;
using EmployeesManagerBL;
using EmployeesManagerBL.Model;

namespace EmployeesManagerUI
{
    public partial class EmployeeForm : Form
    {
        private readonly int _employeeId;
        public EmployeeForm(Employee employee = null, bool readOnly = false)
        {
            InitializeComponent();
            //если передан объект employee
            if (employee != null)
            {
                //заполняем значения контролов из объекта employee
                _employeeId = employee.Id;
                patronymicTB.Text = employee.FirstName;
                lastNameTB.Text = employee.LastName;
                firstNameTB.Text = employee.Patronymic;
                birthdayDTP.Value = employee.DateOfBirth;
                addressTB.Text = employee.AddressOfResidence;
                departmentTB.Text = employee.Department;
                aboutMeTB.Text = employee.AboutMe;
                
                if (readOnly)
                {
                    //отключаем возможность изменения значений текстовых полей и делаем кнопки невидимыми
                    DisableControls();
                }
                else
                {
                    editBtn.Text = "Изменить";
                    editBtn.Click += EditBtn_Click;
                }
            }
            else
            {
                editBtn.Text = "Добавить";
                editBtn.Click += EditBtnAdd_Click;
            }
        }

        private void DisableControls()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.ReadOnly = true;
                }
                else if (control is Button button)
                {
                    button.Visible = false;
                }
            }
        }

        private void EditBtnAdd_Click(object sender, EventArgs e)
        {
            if (CheckEmptyFields())
            {
                return;
            }
            EmployeesHandler.AddEmployee(GetEmployeeFromFields());
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //метод для создания объекта Employee из полей
        private Employee GetEmployeeFromFields()
        {
            return new Employee()
            {
                FirstName = patronymicTB.Text,
                LastName = lastNameTB.Text,
                Patronymic = firstNameTB.Text,
                DateOfBirth = birthdayDTP.Value,
                AddressOfResidence = addressTB.Text,
                Department = departmentTB.Text,
                AboutMe = aboutMeTB.Text
            };
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (CheckEmptyFields())
            {
                return;
            }
            EmployeesHandler.EditEmployee(_employeeId, GetEmployeeFromFields());
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //метод для проверки пустых полей и предупреждения пользователя
        private bool CheckEmptyFields()
        {
            foreach (Control control in this.Controls)
            {
                //если контрол является текстовым полем
                if (control is TextBox textBox)
                {
                    //если поле не заполнено
                    if (String.IsNullOrEmpty(textBox.Text))
                    {
                        //выводим предупреждение
                        return MessageBox.Show("Не все поля заполнены. Вы точно хотите продолжить?","Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Cancel;
                    }
                }
            }

            return false;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
