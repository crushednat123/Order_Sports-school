
using System.Windows.Controls;

namespace SportsSchool.Logic
{
    public class FormValidator
    {
        public static bool AreFieldsEmpty(params Control[] controls)
        {
            foreach (var control in controls)
            {
                if (control is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
                    return true;               
            }
            return false;
        }
    }

}
